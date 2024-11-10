namespace UniCabinet.Web.Extension;

/// <summary>
/// Расширение для Сookies
/// </summary>
public class СookiesExtensions
{
    /// <summary>
    /// Настройка Куки аутенфикации
    /// </summary>
    /// <param name="services"></param>
    public static void AddCookies(IServiceCollection services)
    {
        services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Identity/Account/Login";  // Путь к странице входа
            options.LogoutPath = "/Identity/Account/Logout";  // Путь к странице выхода
            options.AccessDeniedPath = "/Identity/Account/AccessDenied";  // Путь для доступа, если не хватает прав

            options.ExpireTimeSpan = TimeSpan.FromDays(10); // Время жизни куки
            options.SlidingExpiration = true; // Обновлять время действия при каждом запросе
            options.Cookie.HttpOnly = true; // Куки доступны только через HTTP


        // Настройка SecurePolicy в зависимости от среды
#if DEBUG
        options.Cookie.SecurePolicy = CookieSecurePolicy.None;
#else
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
#endif

        });
    }
}