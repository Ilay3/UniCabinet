// UniCabinet.Web/Controllers/AdminController.cs
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using UniCabinet.Application.Interfaces;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Domain.DTO;
using UniCabinet.Domain.Entities;
using UniCabinet.Web.Models;
using UniCabinet.Web.ViewModel;

[Authorize(Roles = "Administrator")]
public class AdminController : Controller
{
    private readonly IUserService _userService;
    private readonly IGroupRepository _groupRepository;
    private readonly UserManager<User> _userManager;

    public AdminController(IUserService userService, IGroupRepository groupRepository, UserManager<User> userManager)
    {
        _userService = userService;
        _groupRepository = groupRepository;
        _userManager = userManager;
    }

    public async Task<IActionResult> VerifiedUsers(string role, string query = null, int pageNumber = 1, int pageSize = 2)
    {
        if (string.IsNullOrEmpty(role))
        {
            return RedirectToAction("VerifiedUsers", new { role = "Student" });
        }

        // Получаем всех пользователей, отфильтрованных по роли
        var users = new List<UserDTO>();

        if (role == "Verified")
        {
            users = (await _userService.GetAllUsersAsync())
                    .Where(user => user.Roles.Count == 1 && user.Roles.Contains("Verified"))
                    .ToList();
        }
        else
        {
            users = (await _userService.GetAllUsersAsync())
                    .Where(user => user.Roles.Contains(role))
                    .ToList();
        }

        // Фильтрация по запросу, если он не пустой
        if (!string.IsNullOrEmpty(query))
        {
            users = users
                .Where(user => user.FullName.Contains(query, StringComparison.CurrentCultureIgnoreCase) ||
                               user.Email.Contains(query, StringComparison.CurrentCultureIgnoreCase))
                .ToList();
        }


        // Пагинация
        var totalUsers = users.Count;
        var paginatedUsers = users
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        ViewBag.SelectedRole = role;
        ViewBag.Roles = new List<string> { "Student", "Teacher", "Administrator", "Verified" }
            .Select(r => new SelectListItem { Value = r, Text = r, Selected = r == role })
            .ToList();

        var groups = await _userService.GetAllGroupsAsync();
        ViewBag.Groups = new SelectList(groups, "Id", "Name");

        var paginationModel = new PaginationModel
        {
            CurrentPage = pageNumber,
            TotalPages = (int)Math.Ceiling(totalUsers / (double)pageSize),
            Action = nameof(VerifiedUsers),
            Controller = "Admin",
            RouteValues = new PaginationRouteValues
            {
                Role = role,
                PageSize = pageSize,
                Query = query
            }
        };

        var model = new StudentGroupViewModel
        {
            Users = paginatedUsers,
            Groups = groups,
            Pagination = paginationModel
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateUserGroup(string userId, int groupId)
    {
        if (!string.IsNullOrEmpty(userId) && groupId > 0)
        {
            // Получаем пользователя
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return RedirectToAction("VerifiedUsers");
            }

            // Проверяем, что пользователь имеет роли "Student" и "Verified"
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Contains("Student") && roles.Contains("Verified"))
            {
                await _userService.UpdateStudentGroupAsync(userId, groupId);
            }
            else
            {
                // Если нет необходимых ролей, перенаправляем с сообщением об ошибке
                TempData["GroupMessage"] = "Изменение группы доступно только для пользователей с ролями Student и Verified.";
                return RedirectToAction("VerifiedUsers");
            }

            return RedirectToAction("VerifiedUsers");
        }

        // Если данные неверные, возвращаемся к списку
        return RedirectToAction("VerifiedUsers");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateUserRoles(string userId, string[] selectedRoles)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        // Получаем текущие роли пользователя
        var currentRoles = await _userManager.GetRolesAsync(user);

        // Роли, которые нужно добавить
        var rolesToAdd = selectedRoles.Except(currentRoles);

        // Роли, которые нужно удалить
        var rolesToRemove = currentRoles.Except(selectedRoles);

        // Удаление ролей, включая роль "Verified", если это необходимо
        if (rolesToRemove.Any())
        {
            var removeResult = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
            if (!removeResult.Succeeded)
            {
                ModelState.AddModelError("", "Ошибка при удалении ролей.");
                return RedirectToAction("VerifiedUsers", new { role = "Student" });
            }
        }

        // Добавление новых ролей, включая роль "Verified"
        if (rolesToAdd.Any())
        {
            var addResult = await _userManager.AddToRolesAsync(user, rolesToAdd);
            if (!addResult.Succeeded)
            {
                ModelState.AddModelError("", "Ошибка при добавлении ролей.");
                return RedirectToAction("VerifiedUsers", new { role = "Student" });
            }
        }

        // Перенаправляем после успешного обновления
        return RedirectToAction("VerifiedUsers", new { role = ViewBag.SelectedRole });
    }

    [HttpGet]
    public async Task<IActionResult> SearchUsers(string query, string role)
    {
        var users = await _userService.GetAllUsersAsync();

        // Фильтрация по роли
        if (role == "Verified")
        {
            users = users.Where(user => user.Roles.Count == 1 && user.Roles.Contains("Verified")).ToList();
        }
        else
        {
            users = users.Where(user => user.Roles.Contains(role)).ToList();
        }

        // Фильтрация по запросу
        if (!string.IsNullOrEmpty(query))
        {
            users = users.Where(user =>
                user.FullName.Contains(query, StringComparison.CurrentCultureIgnoreCase) ||
                user.Email.Contains(query, StringComparison.CurrentCultureIgnoreCase)
            ).ToList();
        }

        // Ограничение количества результатов (например, 10)
        users = users.Take(10).ToList();

        var result = users.Select(user => new
        {
            id = user.Id,
            fullName = user.FullName,
            email = user.Email
        });

        return Json(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserDetails(string userId)
    {
        var userDetailDTO = await _userService.GetUserDetailsAsync(userId);
        if (userDetailDTO == null)
        {
            return NotFound();
        }

        var viewModel = new UserDetailViewModel
        {
            Id = userDetailDTO.Id,
            Email = userDetailDTO.Email,
            FirstName = userDetailDTO.FirstName,
            LastName = userDetailDTO.LastName,
            Patronymic = userDetailDTO.Patronymic,
            DateBirthday = userDetailDTO.DateBirthday,
            Roles = userDetailDTO.Roles,
            GroupName = userDetailDTO.GroupName
        };

        return PartialView("_UserDetailModal", viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateUserDetails(UserDetailViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Некорректные данные.");
        }

        var userDetailDTO = new UserDetailDTO
        {
            Id = model.Id,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Patronymic = model.Patronymic,
            DateBirthday = model.DateBirthday
        };

        await _userService.UpdateUserDetailsAsync(userDetailDTO);
        return Ok();
    }

}