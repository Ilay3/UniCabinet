using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.Entites;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data;

namespace UniCabinet.Infrastructure.Implementations.Repository;

public class ExamRepository : IExamRepository
{
    private readonly ApplicationDbContext _context;
    public ExamRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public ExamDTO GetExamById(int id)
    {
        var examEntity = _context.Exams.Find(id);
        if (examEntity == null) return null;

        return new ExamDTO
        {
            Date = examEntity.Date,
            DisciplineDetailId = examEntity.DisciplineDetailId,
        };
    }

    public List<ExamDTO> GetAllExams()
    {
        var examEntity = _context.Exams.ToList();

        return examEntity.Select(d => new ExamDTO
        {
            Id = d.Id,
            Date = d.Date,
            DisciplineDetailId = d.DisciplineDetailId,
        }).ToList();
    }

    public void AddExam(ExamDTO examDTO)
    {
        var examEntity = new ExamEntity
        {
            Date = examDTO.Date,
            DisciplineDetailId = examDTO.DisciplineDetailId,
        };

        _context.Exams.Add(examEntity);
        _context.SaveChanges();
    }

    public void DeleteExam(int id)
    {
        var examEntity = _context.Exams.Find(id);
        if (examEntity != null)
        {
            _context.Exams.Remove(examEntity);
            _context.SaveChanges();
        }
    }

    public void UpdateExam(ExamDTO examDTO)
    {
        var examEntity = _context.Exams.FirstOrDefault(d => d.Id == examDTO.Id);
        if (examEntity == null) return;

        examEntity.Date = examDTO.Date;
        examEntity.DisciplineDetailId = examDTO.DisciplineDetailId;

        _context.SaveChanges();
    }
}
