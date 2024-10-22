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
    }
}
