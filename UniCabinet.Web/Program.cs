using Microsoft.EntityFrameworkCore;
using UniCabinet.Infrastructure;
using UniCabinet.Infrastructure.Data;
using UniCabinet.Web.DataSending;
using UniCabinet.Web.Extension;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Настройка минимального уровня логирования для Entity Framework Core
builder.Logging.AddConsole();

// ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // Подключение к базе данных
    options.UseSqlServer(connectionString);
    // Включить логирование чувствительных данных только в dev-среде
#if DEBUG
    options.EnableSensitiveDataLogging();
#endif
});

СookiesExtensions.AddCookies(builder.Services);
SessionsExtensions.AddSession(builder.Services);
IdentityExtensions.AddIdentity(builder.Services);


InfrastructureServicesExtensions.AddInfrastructureLayer(builder.Services);
ApplicationsServicesExtensions.AddApplicationLayer(builder.Services);

// Регистрация контроллеров API
builder.Services.AddControllers();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();
await DatabaseSeeder.SeedDatabaseAsync(app);


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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
