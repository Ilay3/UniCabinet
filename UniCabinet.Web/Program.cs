using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using UniCabinet.Application.Interfaces.Services;
using UniCabinet.Application.Services;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data;
using UniCabinet.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString)
           .EnableSensitiveDataLogging()
           .LogTo(Console.WriteLine, LogLevel.Information));

// Настройка куки аутентификации
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";  // Путь к странице входа
    options.LogoutPath = "/Identity/Account/Logout";  // Путь к странице выхода
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";  // Путь для доступа, если не хватает прав

    options.ExpireTimeSpan = TimeSpan.FromDays(10); // Время жизни куки
    options.SlidingExpiration = true; // Обновлять время действия при каждом запросе
    options.Cookie.HttpOnly = true; // Куки доступны только через HTTP
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Использовать куки только по HTTPS
});

// Настройка сессий
builder.Services.AddDistributedMemoryCache();  // Для использования в памяти
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Время жизни сессии
    options.Cookie.HttpOnly = true; // Куки доступны только через HTTP
    options.Cookie.IsEssential = true; // Куки обязательны для работы приложения
});

// Настройка Identity
builder.Services.AddDefaultIdentity<User>(options =>
{
    // Настройки блокировки
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15); // Время блокировки
    options.Lockout.MaxFailedAccessAttempts = 5; // Максимальное количество попыток
    options.Lockout.AllowedForNewUsers = true; // Разрешить блокировку для новых пользователей
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Другие сервисы
builder.Services.AddScoped<IUserVerificationService, UserVerificationService>();
builder.Services.AddSingleton<IEmailSender, EmailSender>();

builder.Services.AddRazorPages();

// MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();


// Инициализация ролей и администратора при запуске приложения
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DataInitializer.SeedRolesAndAdmin(services);
}



// Middleware конфигурация
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
