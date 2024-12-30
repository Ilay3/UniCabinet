using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.LectureManagement;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data;

namespace UniCabinet.Infrastructure.Implementations.Repository
{
    public class LectureRepositoryImpl : ILectureRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public LectureRepositoryImpl(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<LectureDTO> GetLectureByIdAsync(int id)
        {
            var lectureEntity = await _context.Lectures.FindAsync(id);
            if (lectureEntity == null) return null;

            return _mapper.Map<LectureDTO>(lectureEntity);
        }

        public async Task<List<LectureDTO>> GetLectureListByDisciplineDetailIdAsync(int id)
        {
            var lectureListEntity = await _context.Lectures
                .Where(l => l.DisciplineDetailId == id)
                .ToListAsync();

            return _mapper.Map<List<LectureDTO>>(lectureListEntity);

        }


        public async Task AddLectureAsync(LectureDTO lectureDTO)
        {
            var lectureEntity = new LectureEntity
            {
                Date = lectureDTO.Date,
                DisciplineDetailId = lectureDTO.DisciplineDetailId,
                Name = lectureDTO.Name,
            };

            await _context.Lectures.AddAsync(lectureEntity);
            await _context.SaveChangesAsync();
        }



        public async Task UpdateLectureAsync(LectureDTO lectureDTO)
        {
            var lectureEntity = await _context.Lectures.FirstOrDefaultAsync(d => d.Id == lectureDTO.Id);
            if (lectureEntity == null) return;

            lectureEntity.Name = lectureDTO.Name;
            lectureEntity.DisciplineDetailId = lectureDTO.DisciplineDetailId;
            lectureEntity.Date = lectureDTO.Date;

            _context.Lectures.Update(lectureEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetLectureCountByDisciplineDetailIdAsync(int disciplineDetailId)
        {
            return await _context.Lectures.CountAsync(l => l.DisciplineDetailId == disciplineDetailId);
        }
        public async Task<List<LectureEntity>> GetUnprocessedLecturesForDateAsync(DateTime date)
        {
            return await _context.Lectures
                .Where(l => l.Date.Date == date.Date && !l.IsProcessed)
                .Include(l => l.LectureVisits)
                .Include(d=>d.DisciplineDetails)
                .ToListAsync();

        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
