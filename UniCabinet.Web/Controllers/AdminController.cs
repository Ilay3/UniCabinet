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

    public async Task<IActionResult> VerifiedUsers(string role)
    {
        if (string.IsNullOrEmpty(role))
        {
            return RedirectToAction("VerifiedUsers", new { role = "Student" });
        }

        var users = new List<UserDTO>();

        if (role == "Verified")
        {
            users = (await _userService.GetAllUsersAsync())
                    .Where(user => user.Roles.Count == 1 && user.Roles.Contains("Verified"))
                    .ToList();
        }
        else
        {
            // Фильтрация пользователей по выбранной роли
            users = (await _userService.GetAllUsersAsync())
                    .Where(user => user.Roles.Contains(role))
                    .ToList();
        }

        // Сохраняем выбранную роль в ViewBag
        ViewBag.SelectedRole = role;

        // Передаем список ролей для фильтрации
        var roles = new List<string> { "Student", "Teacher", "Administrator", "Verified" };
        ViewBag.Roles = roles.Select(r => new SelectListItem { Value = r, Text = r }).ToList();

        var groups = await _userService.GetAllGroupsAsync();
        ViewBag.Groups = new SelectList(groups, "Id", "Name");

        var model = new StudentGroupViewModel
        {
            Users = users,
            Groups = groups
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
        if (string.IsNullOrEmpty(query))
        {
            return Json(new List<object>());
        }

        var users = await _userService.SearchUsersByNameOrEmailAndRoleAsync(query, role);

        // Возвращаем список пользователей для автодополнения
        var result = users.Select(user => new
        {
            id = user.Id,
            fullName = $"{user.FirstName} {user.LastName} {user.Patronymic}",
            email = user.Email
        }).ToList();

        return Json(result);
    }

}