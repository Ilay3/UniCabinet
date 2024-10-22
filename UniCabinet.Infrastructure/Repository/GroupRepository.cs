// Файл: UniCabinet.Infrastructure/Repository/GroupRepository.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Domain.DTO;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace UniCabinet.Infrastructure.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ISemesterRepository _semesterRepository;
        private readonly ILogger<GroupRepository> _logger;

        public GroupRepository(ApplicationDbContext context, ISemesterRepository semesterRepository, ILogger<GroupRepository> logger)
        {
            _context = context;
            _semesterRepository = semesterRepository;
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
            var currentDate = DateTime.Now;
            var currentSemester = await _semesterRepository.GetCurrentSemesterAsync(currentDate);

            var groupEntity = new Group
            {
                Name = groupDTO.Name,
                TypeGroup = groupDTO.TypeGroup,
                SemesterId = currentSemester.Id,
                CourseId = groupDTO.CourseId,
            };

            await _context.Groups.AddAsync(groupEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGroupAsync(int id)
        {
            var groupEntity = await _context.Groups.FindAsync(id);
            if (groupEntity != null)
            {
                _context.Groups.Remove(groupEntity);
                await _context.SaveChangesAsync();
            }
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

        /// <summary>
        /// Пакетное обновление SemesterId для групп
        /// </summary>
        /// <param name="groupsToUpdate">Список групп для обновления</param>
        /// <returns></returns>
        public async Task UpdateGroupsSemesterAsync(List<GroupDTO> groupsToUpdate)
        {
            if (groupsToUpdate == null || !groupsToUpdate.Any()) return;

            // Преобразуем DTO в сущности с только необходимыми свойствами
            var groupEntities = groupsToUpdate.Select(dto => new Group
            {
                Id = dto.Id,
                SemesterId = dto.SemesterId
            }).ToList();

            try
            {
                // Используем BulkUpdate для пакетного обновления только SemesterId
                await _context.BulkUpdateAsync(groupEntities, new BulkConfig
                {
                    PreserveInsertOrder = true,
                    SetOutputIdentity = true,
                    BatchSize = 1000,
                    PropertiesToInclude = new List<string> { "SemesterId" },
                    UpdateByProperties = new List<string> { "Id" }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при пакетном обновлении SemesterId групп.");
                throw;
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
