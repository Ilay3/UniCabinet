using Microsoft.AspNetCore.Identity;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data;

namespace UniCabinet.Web.DataSending
{
        /// <summary>
        /// Инициализация базы данных при запуске приложения.
        /// </summary>
    public class DatabaseSeeder
    {
        /// <summary>
        /// Заполненеие БД начальными данными, роли и администратора.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static async Task SeedDatabaseAsync(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                var userManager = services.GetRequiredService<UserManager<UserEntity>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();


                await ContextSeed.SeedRolesAsync(roleManager);
                await ContextSeed.SeedAdminAsync(userManager, roleManager);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error occurred seeding the DB.");
            }
        }
    }
}
