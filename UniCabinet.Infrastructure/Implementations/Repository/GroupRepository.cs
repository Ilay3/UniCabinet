using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.CourseManagement;
using UniCabinet.Core.DTOs.UserManagement;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data;

namespace UniCabinet.Infrastructure.Implementations.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<GroupRepository> _logger;

        public GroupRepository(ApplicationDbContext context, ILogger<GroupRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<GroupDTO> GetGroupByIdAsync(int id)
        {
            var groupEntity = await _context.Groups.FindAsync(id);
            if (groupEntity == null) return null;

            return new GroupDTO
            {
                Id = groupEntity.Id,
                Name = groupEntity.Name,
                CourseId = groupEntity.CourseId,
                SemesterId = groupEntity.SemesterId,
                TypeGroup = groupEntity.TypeGroup,
            };
        }
        public async Task<List<GroupDTO>> GetGroupsByIdsAsync(List<int> groupIds)
        {
            var groupEntity = await _context.Groups
                .Where(g => groupIds.Contains(g.Id))
                .ToListAsync();

            return groupEntity.Select(g => new GroupDTO
            {
                Id = g.Id,
                Name = g.Name,
                CourseId = g.CourseId,
                SemesterId = g.SemesterId,
                TypeGroup = g.TypeGroup,
            }).ToList();

        }

        public async Task<List<GroupDTO>> GetAllGroupsAsync()
        {
            var groupEntities = await _context.Groups.ToListAsync();

            return groupEntities.Select(d => new GroupDTO
            {
                Id = d.Id,
                Name = d.Name,
                CourseId = d.CourseId,
                SemesterId = d.SemesterId,
                TypeGroup = d.TypeGroup,
            }).ToList();
        }

        public async Task AddGroupAsync(GroupDTO groupDTO)
        {
            var groupEntity = new GroupEntity
            {
                Name = groupDTO.Name,
                TypeGroup = groupDTO.TypeGroup,
                SemesterId = groupDTO.SemesterId,
                CourseId = groupDTO.CourseId,
            };

            await _context.Groups.AddAsync(groupEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGroupAsync(GroupDTO groupDTO)
        {
            var groupEntity = await _context.Groups.FirstOrDefaultAsync(d => d.Id == groupDTO.Id);
            if (groupEntity == null) return;

            groupEntity.Name = groupDTO.Name;
            groupEntity.TypeGroup = groupDTO.TypeGroup;
            groupEntity.CourseId = groupDTO.CourseId;
            groupEntity.SemesterId = groupDTO.SemesterId;

            _context.Groups.Update(groupEntity);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGroupsSemesterAsync(List<GroupDTO> groupsToUpdate)
        {
            if (groupsToUpdate == null || !groupsToUpdate.Any()) return;

            var groupEntities = groupsToUpdate.Select(dto => new GroupEntity
            {
                Id = dto.Id,
                SemesterId = dto.SemesterId
            }).ToList();

            try
            {
                await _context.BulkUpdateAsync(groupEntities, new BulkConfig
                {
                    PropertiesToInclude = new List<string> { "SemesterId" },
                    UpdateByProperties = new List<string> { "Id" },
                    BatchSize = 1000
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при пакетном обновлении групп.");
                throw;
            }
        }

        public async Task<List<UserDTO>> GetUsersByGroupIdAsync(int groupId)
        {
            var users = await _context.Users.Where(u => u.GroupId == groupId).ToListAsync();

            return users.Select(u => new UserDTO
            {
                Id = u.Id,
                GroupId = u.GroupId,
            }).ToList();
        }

        public async Task UpdateGroupsCourseAsync(List<GroupDTO> groupsToUpdate)
        {
            if (groupsToUpdate == null || !groupsToUpdate.Any()) return;

            var groupEntities = groupsToUpdate.Select(dto => new GroupEntity
            {
                Id = dto.Id,
                CourseId = dto.CourseId
            }).ToList();

            try
            {
                await _context.BulkUpdateAsync(groupEntities, new BulkConfig
                {
                    PropertiesToInclude = new List<string> { "CourseId" },
                    UpdateByProperties = new List<string> { "Id" },
                    BatchSize = 1000
                });
                _logger.LogInformation($"Пакетное обновление курсов выполнено для {groupsToUpdate.Count} групп.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при пакетном обновлении курсов групп.");
                throw;
            }
        }

        public async Task UpdateUsersGroupAsync(List<UserDTO> usersToUpdate)
        {
            if (usersToUpdate == null || !usersToUpdate.Any()) return;

            var userEntities = usersToUpdate.Select(dto => new UserEntity
            {
                Id = dto.Id,
                GroupId = dto.GroupId
            }).ToList();

            try
            {
                await _context.BulkUpdateAsync(userEntities, new BulkConfig
                {
                    PropertiesToInclude = new List<string> { "GroupId" },
                    UpdateByProperties = new List<string> { "Id" },
                    BatchSize = 1000
                });
                _logger.LogInformation($"Пакетное обновление GroupId выполнено для {usersToUpdate.Count} пользователей.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при пакетном обновлении GroupId у пользователей.");
                throw;
            }
        }
    }
}
