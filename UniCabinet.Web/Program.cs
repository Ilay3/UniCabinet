using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using UniCabinet.Application.Interfaces;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Application.Interfaces.Services;
using UniCabinet.Application.Services;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.BackgroundServices;
using UniCabinet.Infrastructure.Data;
using UniCabinet.Infrastructure.Data.Repository;
using UniCabinet.Infrastructure.Repositories;
using UniCabinet.Infrastructure.Repository;
using UniCabinet.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Настройка минимального уровня логирования для Entity Framework Core
builder.Logging.ClearProviders(); // Очищает все провайдеры логов
builder.Logging.AddConsole();
builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);

// ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var environment = builder.Environment;

    // Подключение к базе данных
    options.UseSqlServer(connectionString);

    // Включить логирование чувствительных данных только в dev-среде
    if (environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
    }
});

// Настройка куки аутентификации
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";  // Путь к странице входа
    options.LogoutPath = "/Identity/Account/Logout";  // Путь к странице выхода
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";  // Путь для доступа, если не хватает прав

    options.ExpireTimeSpan = TimeSpan.FromDays(10); // Время жизни куки
    options.SlidingExpiration = true; // Обновлять время действия при каждом запросе
    options.Cookie.HttpOnly = true; // Куки доступны только через HTTP

    // Настройка SecurePolicy в зависимости от среды
    if (builder.Environment.IsDevelopment())
    {
        options.Cookie.SecurePolicy = CookieSecurePolicy.None;
    }
    else
    {
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    }
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

    // Настройки пароля
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;

    options.SignIn.RequireConfirmedAccount = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Регистрация репозиториев
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ISemesterRepository, SemesterRepository>();
builder.Services.AddScoped<IDisciplineRepository, DisciplineRepository>();
builder.Services.AddScoped<IDisciplineDetailRepository, DisciplineDetailRepository>();
builder.Services.AddScoped<ILectureRepository, LectureRepository>();

// Регистрация сервисов
builder.Services.AddScoped<IUserVerificationService, UserVerificationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ISemesterService, SemesterService>();
builder.Services.AddScoped<ILectureService, LectureService>();


// Регистрация сервиса отправки электронной почты
builder.Services.AddSingleton<IEmailSender, EmailSender>();

// Регистрация фонового сервиса обновления семестров
builder.Services.AddHostedService<SemesterBackgroundService>();
builder.Services.AddHostedService<CourseBackgroundService>();

// Регистрация контроллеров API
builder.Services.AddControllers();

// Добавление контроллеров с представлениями и Razor Pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

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

// Маршрутизация Razor Pages и MVC контроллеров
app.MapRazorPages();
app.MapControllers(); // Размещено после MapRazorPages
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=DisciplineDetail}/{action=DisciplineDetailsList}/{id?}");

app.Run();
