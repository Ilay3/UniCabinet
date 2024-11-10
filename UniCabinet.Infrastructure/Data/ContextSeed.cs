using Microsoft.AspNetCore.Identity;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Infrastructure.Data
{
    public class ContextSeed
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            // Проверка существования ролей
            var roles = new[] { "Not Verified", "Verified", "Administrator", "Student", "Teacher" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }


        }
        public static async Task SeedAdminAsync(UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@university.com";
            string adminPassword = "Admin@123";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new UserEntity
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    Id = Guid.NewGuid().ToString(),
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
                _ = !userRoles.Contains("Administrator") ? await userManager.AddToRoleAsync(adminUser, "Administrator") : null;

                _ = !userRoles.Contains("Verified") ? await userManager.AddToRoleAsync(adminUser, "Verified") : null;
            }

        }
    }
}
