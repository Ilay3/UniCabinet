using Microsoft.EntityFrameworkCore;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.Entites;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data;

namespace UniCabinet.Infrastructure.Implementations.Repository
{
    public class LectureRepositoryImpl : ILectureRepository
    {
        private readonly ApplicationDbContext _context;

        public LectureRepositoryImpl(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LectureDTO> GetLectureByIdAsync(int id)
        {
            var lectureEntity = await _context.Lectures.FindAsync(id);
            if (lectureEntity == null) return null;

            return new LectureDTO
            {
                Id = lectureEntity.Id,
                Date = lectureEntity.Date,
                DisciplineDetailId = lectureEntity.DisciplineDetailId,
                Number = lectureEntity.Number,
                PointsCount = lectureEntity.PointsCount
            };
        }

        public async Task<List<LectureDTO>> GetLectureListByDisciplineDetailIdAsync(int id)
        {
            var lectureListEntity = await _context.Lectures
                .Where(l => l.DisciplineDetailId == id)
                .ToListAsync();

            return lectureListEntity.Select(l => new LectureDTO
            {
                Id = l.Id,
                Date = l.Date,
                DisciplineDetailId = l.DisciplineDetailId,
                Number = l.Number,
                PointsCount = l.PointsCount,
            }).ToList();
        }

        public async Task<List<LectureDTO>> GetAllLecturesAsync()
        {
            var lectureEntity = await _context.Lectures.ToListAsync();

            return lectureEntity.Select(d => new LectureDTO
            {
                Id = d.Id,
                Date = d.Date,
                DisciplineDetailId = d.DisciplineDetailId,
                Number = d.Number,
                PointsCount = d.PointsCount
            }).ToList();
        }

        public async Task AddLectureAsync(LectureDTO lectureDTO)
        {
            var lectureEntity = new LectureEntity
            {
                Date = lectureDTO.Date,
                DisciplineDetailId = lectureDTO.DisciplineDetailId,
                Number = lectureDTO.Number,
                PointsCount = lectureDTO.PointsCount
            };

            await _context.Lectures.AddAsync(lectureEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLectureAsync(int id)
        {
            var lectureEntity = await _context.Lectures.FindAsync(id);
            if (lectureEntity != null)
            {
                _context.Lectures.Remove(lectureEntity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateLectureAsync(LectureDTO lectureDTO)
        {
            var lectureEntity = await _context.Lectures.FirstOrDefaultAsync(d => d.Id == lectureDTO.Id);
            if (lectureEntity == null) return;

            lectureEntity.Number = lectureDTO.Number;
            lectureEntity.DisciplineDetailId = lectureDTO.DisciplineDetailId;
            lectureEntity.Date = lectureDTO.Date;
            lectureEntity.PointsCount = lectureDTO.PointsCount;

            _context.Lectures.Update(lectureEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetLectureCountByDisciplineDetailIdAsync(int disciplineDetailId)
        {
            return await _context.Lectures.CountAsync(l => l.DisciplineDetailId == disciplineDetailId);
        }
    }
}
