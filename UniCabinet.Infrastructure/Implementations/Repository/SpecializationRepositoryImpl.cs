using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UniCabinet.Application.Interfaces;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.CourseManagement;
using UniCabinet.Core.DTOs.SpecializationManagement;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data;

namespace UniCabinet.Infrastructure.Implementations.Repository
{
    public class SpecializationRepositoryImpl : ISpecializationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public SpecializationRepositoryImpl(
            ApplicationDbContext context,
           IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SpecializationDTO>> GetAllSpecialization()
        {
            var specializationEntity = await _context.Specialties.ToListAsync();
            return _mapper.Map<List<SpecializationDTO>>(specializationEntity);
        }
        public async Task<List<SpecializationListDTO>> GetDataSpecializationAndTeacher()
        {

            var specializationEntity = await _context.Specialties
                .Include(t => t.Teachers)
                .ToListAsync();
            return _mapper.Map<List<SpecializationListDTO>>(specializationEntity);
        }

        public async Task<SpecializationDTO> GetSpecializationById(int id)
        {
            var specialization = await _context.Specialties.Include(s => s.Teachers).FirstOrDefaultAsync(s => s.Id == id);
            if (specialization == null)
            {
                throw new KeyNotFoundException($"Specialization with ID {id} not found.");
            }

            return _mapper.Map<SpecializationDTO>(specialization);
        }

        public async Task AddSpecialization(SpecializationAddDTO specializationDTO)
        {
            var entity = _mapper.Map<SpecialtyEntity>(specializationDTO);
            await _context.Specialties.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSpecialization(SpecializationEditDTO specializationDTO)
        {
            var entity = await _context.Specialties.FirstOrDefaultAsync(s => s.Id == specializationDTO.Id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Specialization with ID {specializationDTO.Id} not found.");
            }

            _mapper.Map(specializationDTO, entity);
            _context.Specialties.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<UserSpecialtiesAndDisciplinesDTO> GetSpecializationByTeacherId(string teachid)
        {
            // Получаем пользователя
            var user = await _context.Set<UserEntity>()
                .Include(u => u.DepartmentEntity)
                .FirstOrDefaultAsync(u => u.Id == teachid);

            if (user == null)
            {
                throw new KeyNotFoundException("Пользователь не найден.");
            }

            var userDepartmentId = user.DepartmentId;

            // Получаем специальности
            var specialties = await _context.Set<SpecialtyEntity>()
                .Where(s => s.Teachers.Any(t => t.DepartmentId == userDepartmentId))
                .ToListAsync();

            // Извлекаем Id специальностей
            var specialtyIds = specialties.Select(s => s.Id).ToList();

            // Получаем дисциплины
            var disciplines = await _context.Set<DisciplineEntity>()
                .Where(d => specialtyIds.Contains(d.SpecialtyId ?? 0) && d.DepartmentId == userDepartmentId)
                .ToListAsync();

            // Маппинг данных
            return new UserSpecialtiesAndDisciplinesDTO
            {
                Specialties = _mapper.Map<List<SpecializationDTO>>(specialties),
                Disciplines = _mapper.Map<List<DisciplineDTO>>(disciplines)
            };
        }

    }
}
