// UniCabinet.Web/Controllers/AdminController.cs
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using UniCabinet.Application.Interfaces;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Web.ViewModel;

[Authorize(Roles = "Administrator")]
public class AdminController : Controller
{
    private readonly IUserService _userService;
    private readonly IGroupRepository _groupRepository;

    public AdminController(IUserService userService, IGroupRepository groupRepository)
    {
        _userService = userService;
        _groupRepository = groupRepository;
    }

    public async Task<IActionResult> VerifiedUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        var groups = await _userService.GetAllGroupsAsync();

        ViewBag.Groups = new SelectList(groups, "Id", "Name");

        var model = new StudentGroupViewModel
        {
            Students = users,
            Groups = groups
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateUserGroup(string userId, int groupId)
    {
        if (!string.IsNullOrEmpty(userId) && groupId > 0)
        {
            await _userService.UpdateStudentGroupAsync(userId, groupId);
            return RedirectToAction("VerifiedUsers");  // Перенаправление на список пользователей после обновления
        }

        // Если данные неверные, возвращаемся к списку
        return RedirectToAction("VerifiedUsers");
    }


}
