using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using UniCabinet.Domain.Entities;

public static class DataInitializer
{
    public static async Task SeedRolesAndAdmin(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

        // Проверка существования ролей
        var roles = new[] { "Not Verified", "Verified", "Administrator", "Student", "Teacher" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // Проверка существования администратора
        string adminEmail = "admin@university.com";
        string adminPassword = "Admin@123";

        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new User
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Administrator");
                await userManager.AddToRoleAsync(adminUser, "Verified");
            }
        }
        else
        {
            var userRoles = await userManager.GetRolesAsync(adminUser);
            if (!userRoles.Contains("Administrator"))
            {
                await userManager.AddToRoleAsync(adminUser, "Administrator");
            }
            if (!userRoles.Contains("Verified"))
            {
                await userManager.AddToRoleAsync(adminUser, "Verified");
            }
        }
    }
}
