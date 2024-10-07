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
        private readonly ApplicationDbContext _context;

        public UserRepository(UserManager<User> userManager, ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _context = applicationDbContext;
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
                    var group = await _context.Groups.FirstOrDefaultAsync(g => g.Users.Any(s => s.Id == user.Id));
                    if (group != null)
                    {
                        user.Group = group;  // Добавляем группу к пользователю
                    }
                }

                usersWithRoles.Add(user);
            }

            return usersWithRoles;
        }

        public async Task<IEnumerable<User>> GetUsersByRoleAsync(string role)
        {
            return await _userManager.GetUsersInRoleAsync(role);
        }

        public async Task UpdateUserGroupAsync(string userId, int groupId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user != null)
            {
                var group = await _context.Groups.FindAsync(groupId);
                if (group != null)
                {
                    user.GroupId = groupId;
                    _context.Users.Update(user);  // Обновляем пользователя в базе
                    await _context.SaveChangesAsync();  // Сохраняем изменения
                }
            }
        }

        public async Task<IEnumerable<Group>> GetAllGroupsAsync()
        {
            return await _context.Groups.ToListAsync();
        }

        public async Task<IEnumerable<User>> SearchUsersAsync(string query)
        {
            return await _context.Users
                .Where(u => u.FirstName.Contains(query) || u.LastName.Contains(query) || u.Patronymic.Contains(query) || u.Email.Contains(query))
                .ToListAsync();
        }
    }
}
