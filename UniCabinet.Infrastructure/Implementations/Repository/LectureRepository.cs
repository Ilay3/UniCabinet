using Microsoft.EntityFrameworkCore;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.Entites;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data;

namespace UniCabinet.Infrastructure.Implementations.Repository;

public class LectureRepository : ILectureRepository
{
    private readonly ApplicationDbContext _context;
    public LectureRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public LectureDTO GetLectureById(int id)
    {
        var lectureEntity = _context.Lectures.Find(id);
        if (lectureEntity == null) return null;

        return new LectureDTO
        {
            Date = lectureEntity.Date,
            DisciplineDetailId = lectureEntity.DisciplineDetailId,
            Number = lectureEntity.Number,
            PointsCount = lectureEntity.PointsCount
        };
    }

    public async Task<List<LectureDTO>> GetLectureListByDisciplineDetailId(int id)
    {
        var lectureListEntity = _context.Lectures
            .Where(l => l.DisciplineDetailId == id);

        var disciplineDetailEntity = await _context.DisciplineDetails.FindAsync(id);
        var disciplineEntity = await _context.Disciplines.FindAsync(disciplineDetailEntity.DisciplineId);

        return lectureListEntity
            .Select(l => new LectureDTO
            {
                Id = l.Id,
                Date = l.Date,
                DisciplineDetailId = l.DisciplineDetailId,
                Number = l.Number,
                PointsCount = l.PointsCount,
            }).ToList();
    }

    public List<LectureDTO> GetAllLectures()
    {
        var lectureEntity = _context.Lectures.ToList();

        return lectureEntity.Select(d => new LectureDTO
        {
            Id = d.Id,
            Date = d.Date,
            DisciplineDetailId = d.DisciplineDetailId,
            Number = d.Number,
        }).ToList();
    }

    public void AddLecture(LectureDTO lectureDTO)
    {
        var lectureEntity = new LectureEntity
        {
            Date = lectureDTO.Date,
            DisciplineDetailId = lectureDTO.DisciplineDetailId,
            Number = lectureDTO.Number,
            PointsCount = lectureDTO.PointsCount
        };

        _context.Lectures.Add(lectureEntity);
        _context.SaveChanges();
    }

    public async Task DeleteLecture(int id)
    {
        var lectureEntity = await _context.Lectures.FindAsync(id);
        if (lectureEntity != null)
        {
            _context.Lectures.Remove(lectureEntity);
            await _context.SaveChangesAsync();
        }
    }

    public void UpdateLecture(LectureDTO lectureDTO)
    {
        var lectureEntity = _context.Lectures.FirstOrDefault(d => d.Id == lectureDTO.Id);
        if (lectureEntity == null) return;

        lectureEntity.Number = lectureDTO.Number;
        lectureEntity.DisciplineDetailId = lectureDTO.DisciplineDetailId;
        lectureEntity.Date = lectureDTO.Date;
        lectureEntity.PointsCount = lectureDTO.PointsCount;

        _context.SaveChanges();
    }

    public int GetLectureCountByDisciplineDetailId(int disciplineDetailId)
    {
        return _context.Lectures.Count(l => l.DisciplineDetailId == disciplineDetailId);
    }

}
