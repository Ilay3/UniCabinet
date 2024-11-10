using Microsoft.AspNetCore.Identity;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data;

namespace UniCabinet.Web.Extension
{  
    /// <summary>
     /// Расширение для Identity
     /// </summary>
    public class IdentityExtensions
    {
       /// <summary>
       /// Настройка Identity
       /// </summary>
       /// <param name="services"></param>
        public static void AddIdentity(IServiceCollection services)
        {
            services.AddDefaultIdentity<UserEntity>(options =>
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
        } 
    }
}
