// UniCabinet.Infrastructure/Repositories/UserRepository.cs
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UniCabinet.Application.Interfaces;
using UniCabinet.Domain.Entities;


namespace UniCabinet.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<User>> GetVerifiedUsersAsync()
        {
            // Получение всех пользователей с ролью "Verified"
            var users = await _userManager.Users.ToListAsync();
            var filteredUsers = new List<User>();

            foreach (var user in users)
            {
                if (await _userManager.IsInRoleAsync(user, "Verified"))
                {
                    filteredUsers.Add(user);
                }

            }

            return filteredUsers;
        }
    }
}
