using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniCabinet.Application.UseCases;
using UniCabinet.Domain.Entities;

public class ConfirmEmailModel : PageModel
{
    private readonly UserManager<UserEntity> _userManager;

    public ConfirmEmailModel(UserManager<UserEntity> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> OnGetAsync(string userId, string token, [FromServices] UserVerificationUseCase userVerificationUseCase)
    {
        if (userId == null || token == null)
        {
            TempData["ConfirmationResult"] = "Неверная ссылка для подтверждения.";
            return RedirectToPage("/Account/Manage/Index"); // Переходим на страницу профиля
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            TempData["ConfirmationResult"] = $"Не удалось загрузить пользователя с ID '{userId}'.";
            return RedirectToPage("/Account/Manage/Index"); // Переходим на страницу профиля
        }

        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (result.Succeeded)
        {
            var isVerified = await userVerificationUseCase.ExecuteAsync(userId);
            TempData["ConfirmationResult"] = isVerified
                ? "Ваш email успешно подтверждён. Вы прошли верификацию."
                : "Ваш email успешно подтверждён, но данные профиля (ФИО) не заполнены. Пожалуйста, заполните их для завершения верификации.";

            return RedirectToPage("/Account/Manage/Index");
        }

        TempData["ConfirmationResult"] = "Подтверждение email не удалось.";
        return RedirectToPage("/Account/Manage/Index");
    }

}
