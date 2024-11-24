using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.CourseManagement;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data;

namespace UniCabinet.Infrastructure.Implementations.Repository
{
    public class DisciplineRepositoryImpl : IDisciplineRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DisciplineRepositoryImpl(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DisciplineDTO> GetDisciplineByIdAsync(int id)
        {
            var disciplineEntity = await _context.Disciplines.FindAsync(id);
            if (disciplineEntity == null) return null;

            return new DisciplineDTO
            {
                Id = disciplineEntity.Id,
                Name = disciplineEntity.Name,
                Description = disciplineEntity.Description,
            };
        }

        public async Task<List<DisciplineDTO>> GetAllDisciplinesAsync()
        {
            var disciplineEntities = await _context.Disciplines.ToListAsync();

            return disciplineEntities.Select(d => new DisciplineDTO
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
            }).ToList();
        }

        public async Task AddDisciplineAsync(DisciplineDTO disciplineDTO)
        {
            var disciplineEntity = new DisciplineEntity
            {
                Name = disciplineDTO.Name,
                Description = disciplineDTO.Description,
            };

            await _context.Disciplines.AddAsync(disciplineEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDisciplineAsync(int id)
        {
            var disciplineEntity = await _context.Disciplines.FindAsync(id);
            if (disciplineEntity != null)
            {
                _context.Disciplines.Remove(disciplineEntity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateDisciplineAsync(DisciplineDTO disciplineDTO)
        {
            var disciplineEntity = await _context.Disciplines.FirstOrDefaultAsync(d => d.Id == disciplineDTO.Id);
            if (disciplineEntity == null) return;

            disciplineEntity.Name = disciplineDTO.Name;
            disciplineEntity.Description = disciplineDTO.Description;

            _context.Disciplines.Update(disciplineEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<DisciplineDTO>> GetDisciplinesBySpecialtyIdAsync(int? specialtyId)
        {
            if (specialtyId == null)
                return new List<DisciplineDTO>();

            return await _context.Disciplines
                    .Where(d => d.SpecialtyId == specialtyId)
                    .Select(d => new DisciplineDTO
                    {
                        Id = d.Id,
                        Name = d.Name,
                        Description = d.Description,
                        SpecialtyName = d.Specialty.Name
                    })
                    .ToListAsync();
        }
    }
}
