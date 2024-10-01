using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using UniCabinet.Domain.Entities;
using UniCabinet.Application.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace UniCabinet.Web.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IUserVerificationService _verificationService;

        public RegisterModel(UserManager<User> userManager, SignInManager<User> signInManager, IUserVerificationService verificationService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _verificationService = verificationService;
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
                var user = new User { 
                    UserName = Input.Email, 
                    Email = Input.Email,
                    Id = Guid.NewGuid().ToString(), // Явная генерация Id
                    LockoutEnabled = true
                };

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    // Присвоение роли "Не идентифицирован"
                    await _verificationService.AssignRoleAsync(user.Id, "Not Verified");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(Url.Content("~/"));
                }
            }

            return Page();
        }
    }
}
