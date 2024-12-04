using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.DisciplineDetailManagment;
using UniCabinet.Domain.Entities;
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
        public async Task<List<DisciplineDetailDTO>> GetByDisciplineTeacherAndFiltersAsync(int disciplineId, string teacherId, int? courseId, int? semesterId, int? groupId)
        {
            var query = _context.DisciplineDetails
                .Include(dd => dd.Course)
                .Include(dd => dd.Group)
                .Include(dd => dd.Semester)
                .Where(dd => dd.DisciplineId == disciplineId && dd.TeacherId == teacherId);

            if (courseId.HasValue)
            {
                query = query.Where(dd => dd.CourseId == courseId.Value);
            }

            if (semesterId.HasValue)
            {
                query = query.Where(dd => dd.SemesterId == semesterId.Value);
            }

            if (groupId.HasValue)
            {
                query = query.Where(dd => dd.GroupId == groupId.Value);
            }

            var details = await query.ToListAsync();
            return _mapper.Map<List<DisciplineDetailDTO>>(details);
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
        public async Task<List<DisciplineDetailDTO>> GetByDisciplineAndTeacherAsync(int disciplineId, string teacherId)
        {
            var details =  await _context.DisciplineDetails
                .Include(dd => dd.Course)
                .Include(dd => dd.Group)
                .Include(dd => dd.Semester)
                .Where(dd => dd.DisciplineId == disciplineId && dd.TeacherId == teacherId)
                .ToListAsync();

           return _mapper.Map<List<DisciplineDetailDTO>>(details);
        }

        public async Task<DisciplineDetailDTO> GetByIdAsync(int detailId)
        {
            var details = await _context.DisciplineDetails
                .Include(dd => dd.Course)
                .Include(dd => dd.Group)
                .Include(dd => dd.Semester)
                .FirstOrDefaultAsync(dd => dd.Id == detailId);
            return _mapper.Map<DisciplineDetailDTO> (details);

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
