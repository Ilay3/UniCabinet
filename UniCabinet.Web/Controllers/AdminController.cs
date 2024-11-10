// UniCabinet.Web/Controllers/AdminController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniCabinet.Application.Interfaces;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.Entites;
using UniCabinet.Core.Models;
using UniCabinet.Core.Models.ViewModel;
using UniCabinet.Core.Models.ViewModel.User;
using UniCabinet.Domain.Entities;
using UniCabinet.Web.Models.VM.User;

[Authorize(Roles = "Administrator")]
public class AdminController : Controller
{
    private readonly IUserService _userService;
    private readonly IGroupRepository _groupRepository;
    private readonly UserManager<UserEntity> _userManager;


    public AdminController(IUserService userService, IGroupRepository groupRepository, UserManager<UserEntity> userManager)
    {
        _userService = userService;
        _groupRepository = groupRepository;
        _userManager = userManager;
    }

    public async Task<IActionResult> VerifiedUsers(string role, string query = null, int pageNumber = 1, int pageSize = 1)
    {
        if (string.IsNullOrEmpty(role))
        {
            role = "Student"; // По умолчанию выбираем роль Student
        }

        // Получаем всех пользователей (DTO)
        var userDTOs = await _userService.GetAllUsersAsync();

        // Фильтрация по роли
        if (role == "Verified")
        {
            userDTOs = userDTOs.Where(user => user.Roles.Count == 1 && user.Roles.Contains("Verified")).ToList();
        }
        else
        {
            userDTOs = userDTOs.Where(user => user.Roles.Contains(role)).ToList();
        }

        // Фильтрация по запросу
        if (!string.IsNullOrEmpty(query))
        {
            userDTOs = userDTOs
                .Where(user =>
                    (user.FirstName != null && user.FirstName.Contains(query, StringComparison.CurrentCultureIgnoreCase)) ||
                    (user.LastName != null && user.LastName.Contains(query, StringComparison.CurrentCultureIgnoreCase)) ||
                    (user.Patronymic != null && user.Patronymic.Contains(query, StringComparison.CurrentCultureIgnoreCase)) ||
                    user.Email.Contains(query, StringComparison.CurrentCultureIgnoreCase))
                .ToList();
        }

        // Преобразуем DTO в VM
        var userVMs = userDTOs.Select(user => new UserVM
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Patronymic = user.Patronymic,
            DateBirthday = user.DateBirthday,
            Roles = user.Roles,
            GroupName = user.GroupName,
            GroupId = user.GroupId,
            // FullName будет автоматически заполнено благодаря свойству с getter
        }).ToList();

        // Пагинация
        var totalUsers = userVMs.Count;
        var paginatedUsers = userVMs
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        // Подготовка ViewBag для фильтрации
        ViewBag.SelectedRole = role;
        ViewBag.Roles = new List<string> { "Student", "Teacher", "Administrator", "Verified" }
            .Select(r => new SelectListItem { Value = r, Text = r, Selected = r == role })
            .ToList();

        // Получение всех групп
        var groups = await _groupRepository.GetAllGroupsAsync();
        ViewBag.Groups = new SelectList(groups, "Id", "Name");

        // Подготовка модели пагинации
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

        // Создание модели представления
        var model = new StudentGroupVM
        {
            Users = paginatedUsers,
            Groups = groups,
            Pagination = paginationModel
        };

        // Если запрос выполнен через AJAX, возвращаем только частичное представление
        if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            return PartialView("_UserTablePartial", model);
        }

        return View(model);
    }

    // Метод для AJAX поиска
    [HttpGet]
    public async Task<IActionResult> SearchUsers(string query, string role)
    {
        if (string.IsNullOrEmpty(query))
        {
            return Json(new List<UserVM>());
        }

        var userDTOs = await _userService.GetAllUsersAsync();

        // Фильтрация по роли, если необходимо
        if (!string.IsNullOrEmpty(role))
        {
            if (role == "Verified")
            {
                userDTOs = userDTOs.Where(user => user.Roles.Count == 1 && user.Roles.Contains("Verified")).ToList();
            }
            else
            {
                userDTOs = userDTOs.Where(user => user.Roles.Contains(role)).ToList();
            }
        }

        // Фильтрация по запросу
        var filteredUsers = userDTOs
            .Where(user =>
                (user.FirstName != null && user.FirstName.Contains(query, StringComparison.CurrentCultureIgnoreCase)) ||
                (user.LastName != null && user.LastName.Contains(query, StringComparison.CurrentCultureIgnoreCase)) ||
                (user.Patronymic != null && user.Patronymic.Contains(query, StringComparison.CurrentCultureIgnoreCase)) ||
                user.Email.Contains(query, StringComparison.CurrentCultureIgnoreCase))
            .Select(user => new UserVM
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Patronymic = user.Patronymic,
                DateBirthday = user.DateBirthday,
                Roles = user.Roles,
                GroupName = user.GroupName,
                GroupId = user.GroupId,
            })
            .ToList();

        return Json(filteredUsers);
    }


    [HttpGet]
    public async Task<IActionResult> RoleEditModal(string userId)
    {
        var userDTO = await _userService.GetUserByIdAsync(userId);
        if (userDTO == null)
        {
            return NotFound();
        }

        var roles = new List<string> { "Student", "Teacher", "Administrator", "Verified" };

        var model = new UserRolesVM
        {
            UserId = userDTO.Id,
            FullName = $"{userDTO.FirstName} {userDTO.LastName} {userDTO.Patronymic}".Trim(),
            SelectedRoles = userDTO.Roles,
            AvailableRoles = roles.Select(r => new SelectListItem { Value = r, Text = r }).ToList()
        };

        return PartialView("_RoleModal", model);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateUserRoles(UserRolesVM model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user == null)
        {
            return NotFound();
        }

        // Получаем текущие роли пользователя
        var currentRoles = await _userManager.GetRolesAsync(user);

        // Роли, которые нужно добавить
        var rolesToAdd = model.SelectedRoles.Except(currentRoles);

        // Роли, которые нужно удалить
        var rolesToRemove = currentRoles.Except(model.SelectedRoles);

        // Удаление ролей
        if (rolesToRemove.Any())
        {
            var removeResult = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
            if (!removeResult.Succeeded)
            {
                ModelState.AddModelError("", "Ошибка при удалении ролей.");
                return PartialView("_RoleModal", model);
            }
        }

        // Добавление новых ролей
        if (rolesToAdd.Any())
        {
            var addResult = await _userManager.AddToRolesAsync(user, rolesToAdd);
            if (!addResult.Succeeded)
            {
                ModelState.AddModelError("", "Ошибка при добавлении ролей.");
                return PartialView("_RoleModal", model);
            }
        }

        // Возвращаем успех
        return Json(new { success = true });
    }

    [HttpGet]
    public async Task<IActionResult> GroupEditModal(string userId)
    {
        var userDTO = await _userService.GetUserByIdAsync(userId);
        if (userDTO == null)
        {
            return NotFound();
        }

        var groups = await _groupRepository.GetAllGroupsAsync();

        var model = new UserGroupVM
        {
            UserId = userDTO.Id,
            FullName = $"{userDTO.FirstName} {userDTO.LastName} {userDTO.Patronymic}".Trim(),
            GroupId = userDTO.GroupId,
            AvailableGroups = groups.Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name }).ToList()
        };

        return PartialView("_GroupModal", model);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateUserGroup(UserGroupVM model)
    {
        if (string.IsNullOrEmpty(model.UserId) || model.GroupId == null)
        {
            return BadRequest("Некорректные данные.");
        }

        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user == null)
        {
            return NotFound();
        }

        // Проверяем, что пользователь имеет роли "Student" и "Verified"
        var roles = await _userManager.GetRolesAsync(user);
        if (roles.Contains("Student") && roles.Contains("Verified"))
        {
            await _userService.UpdateStudentGroupAsync(model.UserId, model.GroupId.Value);
        }
        else
        {
            ModelState.AddModelError("", "Изменение группы доступно только для пользователей с ролями Student и Verified.");
            return PartialView("_GroupModal", model);
        }

        // Возвращаем успех
        return Json(new { success = true });
    }

    [HttpGet]
    public async Task<IActionResult> UserDetailModal(string userId)
    {
        var userDTO = await _userService.GetUserByIdAsync(userId);
        if (userDTO == null)
        {
            return NotFound();
        }

        var model = new UserDetailVM
        {
            Id = userDTO.Id,
            Email = userDTO.Email,
            FirstName = userDTO.FirstName,
            LastName = userDTO.LastName,
            Patronymic = userDTO.Patronymic,
            DateBirthday = userDTO.DateBirthday
        };

        return PartialView("_UserDetailModal", model);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateUserDetails(UserDetailVM model)
    {
        if (!ModelState.IsValid)
        {
            return PartialView("_UserDetailModal", model);
        }

        // Создаём DTO из VM
        var userDetailDTO = new UserDTO
        {
            Id = model.Id,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Patronymic = model.Patronymic,
            DateBirthday = model.DateBirthday
        };

        await _userService.UpdateUserDetailsAsync(userDetailDTO);

        // Возвращаем успех
        return Json(new { success = true });
    }


}