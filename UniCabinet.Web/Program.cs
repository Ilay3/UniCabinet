using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using UniCabinet.Application.Interfaces;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Application.Interfaces.Services;
using UniCabinet.Application.Services;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data;
using UniCabinet.Infrastructure.Data.Repository;
using UniCabinet.Infrastructure.Repositories;
using UniCabinet.Infrastructure.Repository;
using UniCabinet.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// ��������� ������������ ������ ����������� ��� Entity Framework Core
builder.Logging.ClearProviders(); // ������� ��� ���������� �����
builder.Logging.AddConsole();
builder.Logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);

// ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var environment = builder.Environment;

    // ����������� � ���� ������
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

    // �������� ����������� �������������� ������ ������ � dev-�����
    if (environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
    }
});


// ��������� ���� ��������������
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";  // ���� � �������� �����
    options.LogoutPath = "/Identity/Account/Logout";  // ���� � �������� ������
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";  // ���� ��� �������, ���� �� ������� ����

    options.ExpireTimeSpan = TimeSpan.FromDays(10); // ����� ����� ����
    options.SlidingExpiration = true; // ��������� ����� �������� ��� ������ �������
    options.Cookie.HttpOnly = true; // ���� �������� ������ ����� HTTP
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // ������������ ���� ������ �� HTTPS
});

// ��������� ������
builder.Services.AddDistributedMemoryCache();  // ��� ������������� � ������
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // ����� ����� ������
    options.Cookie.HttpOnly = true; // ���� �������� ������ ����� HTTP
    options.Cookie.IsEssential = true; // ���� ����������� ��� ������ ����������
});

// ��������� Identity
builder.Services.AddDefaultIdentity<User>(options =>
{
    // ��������� ����������
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15); // ����� ����������
    options.Lockout.MaxFailedAccessAttempts = 5; // ������������ ���������� �������
    options.Lockout.AllowedForNewUsers = true; // ��������� ���������� ��� ����� �������������
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// ������ �������
builder.Services.AddScoped<IUserVerificationService, UserVerificationService>();
builder.Services.AddSingleton<IEmailSender, EmailSender>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ISemesterRepository, SemesterRepository>();
builder.Services.AddScoped<IDisciplineRepository, DisciplineRepository>();
builder.Services.AddScoped<IDisciplineDetailRepository, DisciplineDetailRepository>();
builder.Services.AddScoped<ILectureRepository, LectureRepository>();
builder.Services.AddScoped<ILectureService, LectureService>();


builder.Services.AddRazorPages();

// MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();


// ������������� ����� � �������������� ��� ������� ����������
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DataInitializer.SeedRolesAndAdmin(services);
}



// Middleware ������������
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
    pattern: "{controller=DisciplineDetail}/{action=DisciplineDetailsList}/{id?}");

app.Run();
