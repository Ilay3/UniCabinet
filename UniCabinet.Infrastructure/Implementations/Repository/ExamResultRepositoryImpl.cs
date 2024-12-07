using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.ExamManagement;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using UniCabinet.Core.DTOs.UserManagement;

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

        public async Task<ExamResultDTO> GetExamResultByIdAsync(int id)
        {
            var examResultEntity = await _context.ExamResults.FindAsync(id);
            if (examResultEntity == null) return null;

            return _mapper.Map<ExamResultDTO>(examResultEntity);
        }

        public async Task<List<ExamResultDTO>> GetAllExamResultsAsync()
        {
            var examResults = await _context.ExamResults
                .Include(er => er.Student)
                .Include(er => er.Exam)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<ExamResultDTO>>(examResults);
        }

        public async Task AddExamResultAsync(ExamResultDTO examResultDTO)
        {
            var examResultEntity = _mapper.Map<ExamResultEntity>(examResultDTO);

            await _context.ExamResults.AddAsync(examResultEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteExamResultAsync(int id)
        {
            var examResultEntity = await _context.ExamResults.FindAsync(id);
            if (examResultEntity != null)
            {
                _context.ExamResults.Remove(examResultEntity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateExamResultAsync(ExamResultDTO examResultDTO)
        {
            var examResultEntity = await _context.ExamResults.FirstOrDefaultAsync(er => er.Id == examResultDTO.Id);
            if (examResultEntity == null) return;

            _mapper.Map(examResultDTO, examResultEntity);
            _context.ExamResults.Update(examResultEntity);
            await _context.SaveChangesAsync();
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
            // Получаем экзамен по ID
            var exam = await _context.Exams
                .Where(e => e.Id == examId)
                .Include(e => e.DisciplineDetails) 
                .FirstOrDefaultAsync();

            if (exam == null)
            {
                throw new InvalidOperationException($"Exam with ID {examId} not found.");
            }

            var disciplineDetail = exam.DisciplineDetails;

            // Получаем список студентов из группы
            var students = await _context.Users
                .Where(u => u.GroupId == disciplineDetail.GroupId)
                .ToListAsync();


            var examResults = students.Select(student => new ExamResultDTO
            {
                StudentId = student.Id,
                ExamId = examId,
                StudentFirstName = student.FirstName,
                StudentLastName = student.LastName,
                StudentPatronymic = student.Patronymic,
                PointAvarage = 0, 
                FinalPoint = 0,
                IsAutomatic = false
            }).ToList();

            return examResults;
        }

    }
}
