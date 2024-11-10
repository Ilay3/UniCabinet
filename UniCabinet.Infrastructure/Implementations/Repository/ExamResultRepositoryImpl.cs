using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.Entites;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data;

namespace UniCabinet.Infrastructure.Implementations.Repository;

public class ExamResultRepositoryImpl : IExamResultRepository
{
    private readonly ApplicationDbContext _context;
    public ExamResultRepositoryImpl(ApplicationDbContext context)
    {
        _context = context;
    }

    public ExamResultDTO GetExamResultById(int id)
    {
        var examResultEntity = _context.ExamResults.Find(id);
        if (examResultEntity == null) return null;

        return new ExamResultDTO
        {
            ExamId = examResultEntity.ExamId,
            FinalPoint = examResultEntity.FinalPoint,
            IsAutomatic = examResultEntity.IsAutomatic,
            PointAvarage = examResultEntity.PointAvarage,
            StudentId = examResultEntity.StudentId,
        };
    }

    public List<ExamResultDTO> GetAllExamResults()
    {
        var examResultEntity = _context.ExamResults.ToList();

        return examResultEntity.Select(d => new ExamResultDTO
        {
            Id = d.Id,
            ExamId = d.ExamId,
            FinalPoint = d.FinalPoint,
            StudentId = d.StudentId,
            IsAutomatic = d.IsAutomatic,
            PointAvarage = d.PointAvarage,
        }).ToList();
    }

    public void AddExamResult(ExamResultDTO examResultDTO)
    {
        var examResultEntity = new ExamResultEntity
        {
            ExamId = examResultDTO.ExamId,
            StudentId = examResultDTO.StudentId,
            PointAvarage = examResultDTO.PointAvarage,
            FinalPoint = examResultDTO.FinalPoint,
            IsAutomatic = examResultDTO.IsAutomatic,
        };

        _context.ExamResults.Add(examResultEntity);
        _context.SaveChanges();
    }

    public void DeleteExamResult(int id)
    {
        var examResultEntity = _context.ExamResults.Find(id);
        if (examResultEntity != null)
        {
            _context.ExamResults.Remove(examResultEntity);
            _context.SaveChanges();
        }
    }

    public void UpdateExamResult(ExamResultDTO examResultDTO)
    {
        var examResultEntity = _context.ExamResults.FirstOrDefault(d => d.Id == examResultDTO.Id);
        if (examResultEntity == null) return;

        examResultEntity.FinalPoint = examResultDTO.FinalPoint;
        examResultEntity.PointAvarage = examResultDTO.PointAvarage;
        examResultEntity.IsAutomatic = examResultDTO.IsAutomatic;
        examResultEntity.ExamId = examResultDTO.ExamId;
        examResultEntity.StudentId = examResultDTO.StudentId;

        _context.SaveChanges();
    }
}
