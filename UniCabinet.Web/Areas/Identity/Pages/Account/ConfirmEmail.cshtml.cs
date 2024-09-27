using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using UniCabinet.Application.Interfaces;
using UniCabinet.Domain.Entities;

public class ConfirmEmailModel : PageModel
{
    private readonly UserManager<User> _userManager;
    private readonly IUserVerificationService _verificationService;

    public ConfirmEmailModel(UserManager<User> userManager, IUserVerificationService verificationService)
    {
        _userManager = userManager;
        _verificationService = verificationService;
    }

    public async Task<IActionResult> OnGetAsync(string userId, string token)
    {
        if (userId == null || token == null)
        {
            return RedirectToPage("/Index");
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{userId}'.");
        }

        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (result.Succeeded)
        {
            if (await _verificationService.VerifyUserAsync(userId))
            {
                return RedirectToPage("/Account/Manage/VerificationSuccess");
            }
        }

        return BadRequest("Email confirmation failed");
    }

}
