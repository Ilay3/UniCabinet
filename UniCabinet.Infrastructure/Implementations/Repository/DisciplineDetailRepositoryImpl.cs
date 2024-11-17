using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.CourseManagement;
using UniCabinet.Infrastructure.Data;

namespace UniCabinet.Infrastructure.Implementations.Repository
{
    public class DisciplineDetailRepositoryImpl : IDisciplineDetailRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DisciplineDetailRepositoryImpl(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void AddDisciplineDetail(DisciplineDetailDTO disciplineDetailDTO)
        {
            throw new NotImplementedException();
        }

        public void DeleteDisciplineDetail(int id)
        {
            throw new NotImplementedException();
        }

        public List<DisciplineDetailDTO> GetAllDisciplineDetails()
        {
            throw new NotImplementedException();
        }

        public async Task<DisciplineDetailDTO> GetDetailByUserAndDisciplineAsync(string userId, int disciplineId)
        {
            var disciplineDetailEntity = await _context.DisciplineDetails
                .Include(d => d.Discipline)
                .Include(d => d.Group)
                .Include(d => d.Semester)
                .Include(d => d.Course)
                .Include(d => d.Teacher)
                .FirstOrDefaultAsync(d => d.TeacherId == userId && d.DisciplineId == disciplineId);

            return _mapper.Map<DisciplineDetailDTO>(disciplineDetailEntity);

        }

        public Task<DisciplineDetailDTO> GetDisciplineDetailByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateDisciplineDetail(DisciplineDetailDTO disciplineDetailDTO)
        {
            throw new NotImplementedException();
        }
    }
}
