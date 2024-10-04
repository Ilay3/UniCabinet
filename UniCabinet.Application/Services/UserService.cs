// UniCabinet.Application/Services/UserService.cs
using System.Collections.Generic;
using UniCabinet.Application.Interfaces;
using UniCabinet.Domain.DTO;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDTO>> GetVerifiedUsersAsync()
        {
            var users = await _userRepository.GetVerifiedUsersAsync();

            // Маппинг пользователей в DTO
            var verifiedUsers = users.Select(user => new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                FullName = $"{user.FirstName} {user.LastName} {user.Patronymic}",
                Roles = new List<string> { "Verified"}
            }).ToList();

            return verifiedUsers;
        }
    }


}
