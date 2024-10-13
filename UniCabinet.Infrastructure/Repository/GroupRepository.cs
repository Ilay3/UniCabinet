using Microsoft.EntityFrameworkCore;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Domain.DTO;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data;

namespace UniCabinet.Infrastructure.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ApplicationDbContext _context;
        public GroupRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GroupDTO> GetGroupById(int id)
        {
            var groupEntity = await _context.Groups.FindAsync(id);
            if (groupEntity == null) return null;

            return new GroupDTO
            {
                Name = groupEntity.Name,
                CourseId = groupEntity.CourseId,
                SemesterId = groupEntity.SemesterId,
                TypeGroup = groupEntity.TypeGroup,
            };
        }

        public async Task<List<GroupDTO>> GetAllGroups()
        {
            var groupEntity = await _context.Groups.ToListAsync();

            return groupEntity.Select(d => new GroupDTO
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
            var groupEntity = new Group
            {
                Name = groupDTO.Name,
                TypeGroup = groupDTO.TypeGroup,
                SemesterId = groupDTO.SemesterId,
                CourseId = groupDTO.CourseId,
            };

            await _context.Groups.AddAsync(groupEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGroup(int id)
        {
            var groupEntity = await _context.Groups.FindAsync(id);
            if (groupEntity != null)
            {
                _context.Groups.Remove(groupEntity);
                await _context.SaveChangesAsync();
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
    }
}
