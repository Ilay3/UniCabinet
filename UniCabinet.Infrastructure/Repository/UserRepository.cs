// UniCabinet.Infrastructure/Repositories/UserRepository.cs
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UniCabinet.Application.Interfaces;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data;


namespace UniCabinet.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(UserManager<User> userManager, ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<User>> GetAllUsersWithRolesAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var usersWithRoles = new List<User>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Contains("Student"))
                {
                    // Загрузить информацию о группе студента
                    var group = await _applicationDbContext.Groups.FirstOrDefaultAsync(g => g.Users.Any(s => s.Id == user.Id));
                    if (group != null)
                    {
                        user.Group = group;  // Добавляем группу к пользователю
                    }
                }

                usersWithRoles.Add(user);
            }

            return usersWithRoles;
        }

    }
}
