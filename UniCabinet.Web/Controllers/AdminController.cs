// UniCabinet.Web/Controllers/AdminController.cs
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UniCabinet.Application.Interfaces;
using UniCabinet.Application.Interfaces.Repository;

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
        var users = await _userService.GetVerifiedUsersAsync();
        var groups = _groupRepository.GetAllGroups().ToDictionary(b => b.Id, b => b.Name);

        ViewBag.Groups = groups.Values;
        ViewBag.SelectedGroup = groups;
        return View(users);
    }
}
