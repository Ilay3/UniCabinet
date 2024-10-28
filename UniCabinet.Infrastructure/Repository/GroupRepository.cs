using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Domain.DTO;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data;
using EFCore.BulkExtensions;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace UniCabinet.Infrastructure.Repository
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

        // Существующие синхронные методы
        public GroupDTO GetGroupById(int id)
        {
            var groupEntity = _context.Groups.Find(id);
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

        public List<GroupDTO> GetAllGroups()
        {
            var groupEntities = _context.Groups.ToList();

            return groupEntities.Select(d => new GroupDTO
            {
                Id = d.Id,
                Name = d.Name,
                CourseId = d.CourseId,
                SemesterId = d.SemesterId,
                TypeGroup = d.TypeGroup,
            }).ToList();
        }

        public void AddGroup(GroupDTO groupDTO)
        {
            var groupEntity = new Group
            {
                Name = groupDTO.Name,
                TypeGroup = groupDTO.TypeGroup,
                SemesterId = groupDTO.SemesterId,
                CourseId = groupDTO.CourseId,
            };

            _context.Groups.Add(groupEntity);
            _context.SaveChanges();
        }

        public void DeleteGroup(int id)
        {
            var groupEntity = _context.Groups.Find(id);
            if (groupEntity != null)
            {
                _context.Groups.Remove(groupEntity);
                _context.SaveChanges();
            }
        }

        public void UpdateGroup(GroupDTO groupDTO)
        {
            var groupEntity = _context.Groups.FirstOrDefault(d => d.Id == groupDTO.Id);
            if (groupEntity == null) return;

            groupEntity.Name = groupDTO.Name;
            groupEntity.TypeGroup = groupDTO.TypeGroup;
            groupEntity.CourseId = groupDTO.CourseId;
            groupEntity.SemesterId = groupDTO.SemesterId;

            _context.Groups.Update(groupEntity);
            _context.SaveChanges();
        }

        // Добавленные асинхронные методы
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

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Пакетное обновление SemesterId для списка групп.
        /// </summary>
        /// <param name="groupsToUpdate">Список групп для обновления.</param>
        public async Task UpdateGroupsSemesterAsync(List<GroupDTO> groupsToUpdate)
        {
            if (groupsToUpdate == null || !groupsToUpdate.Any()) return;

            var groupEntities = groupsToUpdate.Select(dto => new Group
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
                    BatchSize = 1000 // Настройте размер партии при необходимости
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при пакетном обновлении групп.");
                throw;
            }
        }

        /// <summary>
        /// Получает список пользователей по идентификатору группы.
        /// </summary>
        /// <param name="groupId">Идентификатор группы.</param>
        /// <returns>Список пользователей.</returns>
        public List<UserDTO> GetUsersByGroupId(int groupId)
        {
            var users = _context.Users.Where(u => u.GroupId == groupId).ToList();

            return users.Select(u => new UserDTO
            {
                Id = u.Id,
                GroupId = u.GroupId,
                // Другие свойства, если необходимо
            }).ToList();
        }

        /// <summary>
        /// Обновляет курсы групп пакетно.
        /// </summary>
        /// <param name="groupsToUpdate">Список групп для обновления.</param>
        public void UpdateGroupsCourse(List<GroupDTO> groupsToUpdate)
        {
            if (groupsToUpdate == null || !groupsToUpdate.Any()) return;

            var groupEntities = groupsToUpdate.Select(dto => new Group
            {
                Id = dto.Id,
                CourseId = dto.CourseId
            }).ToList();

            try
            {
                _context.BulkUpdate(groupEntities, new BulkConfig
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

        /// <summary>
        /// Обновляет связь группы у пользователей пакетно.
        /// </summary>
        /// <param name="usersToUpdate">Список пользователей для обновления.</param>
        public void UpdateUsersGroup(List<UserDTO> usersToUpdate)
        {
            if (usersToUpdate == null || !usersToUpdate.Any()) return;

            var userEntities = usersToUpdate.Select(dto => new User
            {
                Id = dto.Id,
                GroupId = dto.GroupId
            }).ToList();

            try
            {
                _context.BulkUpdate(userEntities, new BulkConfig
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
