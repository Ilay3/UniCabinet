using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.ExamManagement;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data;

namespace UniCabinet.Infrastructure.Implementations.Repository
{
    public class ExamResultRepositoryImpl : IExamResultRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ExamResultRepositoryImpl(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        public async Task AddOrUpdateExamResultAsync(ExamResultDTO examResultDTO)
        {
            var existingResult = await _context.ExamResults
                .FirstOrDefaultAsync(er => er.ExamId == examResultDTO.ExamId && er.StudentId == examResultDTO.StudentId);

            if (existingResult != null)
            {
                _mapper.Map(examResultDTO, existingResult);
                _context.ExamResults.Update(existingResult);
            }
            else
            {
                var examResultEntity = _mapper.Map<ExamResultEntity>(examResultDTO);
                await _context.ExamResults.AddAsync(examResultEntity);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<ExamResultDTO>> GetExamResultsByExamIdAsync(int examId)
        {
            var exam = await _context.Exams
    .Where(e => e.Id == examId)
    .Include(e => e.DisciplineDetails)
    .FirstOrDefaultAsync();

            if (exam == null)
            {
                throw new InvalidOperationException($"Exam with ID {examId} not found.");
            }

            var disciplineDetail = exam.DisciplineDetails;

            var students = await _context.Users
    .Where(u => u.GroupId == disciplineDetail.GroupId)
    .ToListAsync();

            var studentProgresses = await _context.StudentProgresses
    .Where(sp => sp.DisciplineDetailId == disciplineDetail.Id)
    .ToListAsync();

            var examResults = students.Select(student =>
            {
                var progress = studentProgresses
                    .FirstOrDefault(sp => sp.StudentId == student.Id);

                return new ExamResultDTO
                {
                    StudentId = student.Id,
                    ExamId = examId,
                    StudentFirstName = student.FirstName,
                    StudentLastName = student.LastName,
                    StudentPatronymic = student.Patronymic,
                    GradeAvarage = progress?.FinalGrade ?? 0,
                    FinalGrade = 0,
                    IsAutomatic = false
                };
            }).ToList();

            return examResults;
        }


    }
}
