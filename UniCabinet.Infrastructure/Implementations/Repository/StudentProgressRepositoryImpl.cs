using AutoMapper;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.EntityFrameworkCore;
using System;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.StudentProgressManagment;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data;

namespace UniCabinet.Infrastructure.Implementations.Repository
{
    public class StudentProgressRepositoryImpl : IStudentProgressRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public StudentProgressRepositoryImpl(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<StudentProgressDTO>> GetAllStudentProgressById(string studentId)
        {
            return await _context.Set<StudentProgressEntity>()
                        .Where(sp => sp.StudentId == studentId)
                        .Select(sp => new StudentProgressDTO
                        {
                            Id = sp.Id,
                            StudentId = sp.StudentId,
                            DisciplineDetailId = sp.DisciplineDetailId,
                            DisciplineName = sp.DisciplineDetails.Discipline.Name,
                            TotalLecturePoints = sp.TotalLecturePoints,
                            TotalPracticalPoints = sp.TotalPracticalPoints,
                            TotalPoints = sp.TotalPoints,
                            FinalGrade = sp.FinalGrade,
                            NeedsRetake = sp.NeedsRetake
                        })
                        .ToListAsync();
        }

    

        public async Task<StudentProgressDTO> GetStudentProgressAsync(string studentId, int disciplineDetailId)
        {
            var progress = await _context.StudentProgresses
                .FirstOrDefaultAsync(sp => sp.StudentId == studentId && sp.DisciplineDetailId == disciplineDetailId);

            return _mapper.Map<StudentProgressDTO>(progress);
        }

        public async Task AddStudentProgressAsync(StudentProgressDTO studentProgress)
        {
           var progressDTO =  _mapper.Map<StudentProgressEntity>(studentProgress);
            await _context.StudentProgresses.AddAsync(progressDTO);
        }
        public async Task UpdateFinalGradeAsync(string studentId, decimal finalGrade)
        {
            var student = await _context.StudentProgresses.FirstOrDefaultAsync(s => s.StudentId == studentId);
            if (student == null)
            {
                throw new Exception($"Студент с ID {studentId} не найден.");
            }
            student.FinalGrade = (int)finalGrade;

            student.NeedsRetake = finalGrade <= 2;

            await SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

      
    }
}
