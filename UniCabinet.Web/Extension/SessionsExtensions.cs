namespace UniCabinet.Web.Extension;

/// <summary>
/// Расширение для Session
/// </summary>
public class SessionsExtensions
{
    /// <summary>
    /// Настройка Сессии
    /// </summary>
    /// <param name="services"></param>
    public static void AddSession(IServiceCollection services)
    {
        services.AddDistributedMemoryCache();  // Для использования в памяти
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30); // Время жизни сессии
            options.Cookie.HttpOnly = true; // Куки доступны только через HTTP
            options.Cookie.IsEssential = true; // Куки обязательны для работы приложения
        });
    }
}
