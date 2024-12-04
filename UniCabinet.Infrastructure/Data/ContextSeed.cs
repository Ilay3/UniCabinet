using Microsoft.AspNetCore.Identity;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Infrastructure.Data
{
    public class ContextSeed
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            // Проверка существования ролей
            var roles = new[] { "Not Verified", "Верефицирован", "Администратор", "Студент", "Преподаватель", "Зав. Кафедры" };
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
                    await userManager.AddToRoleAsync(adminUser, "Администратор");
                    await userManager.AddToRoleAsync(adminUser, "Верефицирован");
                }
            }
            else
            {
                var userRoles = await userManager.GetRolesAsync(adminUser);
                _ = !userRoles.Contains("Администратор") ? await userManager.AddToRoleAsync(adminUser, "Администратор") : null;

                _ = !userRoles.Contains("Верефицирован") ? await userManager.AddToRoleAsync(adminUser, "Верефицирован") : null;
            }

        }
    }
}
