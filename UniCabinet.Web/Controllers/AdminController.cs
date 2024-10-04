// UniCabinet.Web/Controllers/AdminController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UniCabinet.Application.Interfaces;

[Authorize(Roles = "Administrator")]
public class AdminController : Controller
{
    private readonly IUserService _userService;

    public AdminController(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IActionResult> VerifiedUsers()
    {
        var users = await _userService.GetVerifiedUsersAsync();
        return View(users);
    }
}
