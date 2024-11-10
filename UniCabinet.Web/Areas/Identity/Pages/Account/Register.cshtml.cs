using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using UniCabinet.Application.UseCases;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Web.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;
        private readonly UserVerificationUseCase _userVerificationUseCase;
        public RegisterModel(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, UserVerificationUseCase userVerificationUseCase)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userVerificationUseCase = userVerificationUseCase;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new UserEntity
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    Id = Guid.NewGuid().ToString(),
                    LockoutEnabled = true
                };

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    // Присвоение роли "Не идентифицирован"
                    await _userVerificationUseCase.AssignRoleAsync(user.Id, "Not Verified");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(Url.Content("~/"));
                }
            }

            return Page();
        }
    }
}
