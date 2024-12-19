using AutoMapper;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.EntityFrameworkCore;
using System;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.StudentManagement;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data;

namespace UniCabinet.Infrastructure.Implementations.Repository
{
    public class StudentProgressRepository : IStudentProgressRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public StudentProgressRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public StudentProgressDTO GetStudentProfressById(int id)
        {
            var studentProgressEntity = _context.StudentProgresses.Find(id);
            if (studentProgressEntity == null) return null;

            return new StudentProgressDTO
            {
                DisciplineDetailId = studentProgressEntity.DisciplineDetailId,
                StudentId = studentProgressEntity.StudentId,
                FinalGrade = studentProgressEntity.FinalGrade,
                NeedsRetake = studentProgressEntity.NeedsRetake,
                TotalLecturePoints = studentProgressEntity.TotalLecturePoints,
                TotalPoints = studentProgressEntity.TotalPoints,
                TotalPracticalPoints = studentProgressEntity.TotalPracticalPoints,
            };
        }

        public List<StudentProgressDTO> GetAllStudentProgress()
        {
            var studentProgressEntity = _context.StudentProgresses.ToList();

            return studentProgressEntity.Select(d => new StudentProgressDTO
            {
                Id = d.Id,
                StudentId = d.StudentId,
                DisciplineDetailId = d.DisciplineDetailId,
                FinalGrade = d.FinalGrade,
                NeedsRetake = d.NeedsRetake,
                TotalLecturePoints = d.TotalLecturePoints,
                TotalPoints = d.TotalPoints,
                TotalPracticalPoints = d.TotalPracticalPoints,
            }).ToList();
        }

        public void AddStudentProgress(StudentProgressDTO studentProgressDTO)
        {
            var studentProgressEntity = new StudentProgressEntity
            {
                StudentId = studentProgressDTO.StudentId,
                DisciplineDetailId = studentProgressDTO.DisciplineDetailId,
                FinalGrade = studentProgressDTO.FinalGrade,
                NeedsRetake = studentProgressDTO.NeedsRetake,
                TotalLecturePoints = studentProgressDTO.TotalLecturePoints,
                TotalPracticalPoints = studentProgressDTO.TotalPracticalPoints,
                TotalPoints = studentProgressDTO.TotalPoints,
            };

            _context.StudentProgresses.Add(studentProgressEntity);
            _context.SaveChanges();
        }

        public void DeleteStudentProgress(int id)
        {
            var studentProgressEntity = _context.StudentProgresses.Find(id);
            if (studentProgressEntity != null)
            {
                _context.StudentProgresses.Remove(studentProgressEntity);
                _context.SaveChanges();
            }
        }

        public void UpdateStudentProgress(StudentProgressDTO studentProgressDTO)
        {
            var studentProgressEntity = _context.StudentProgresses.FirstOrDefault(d => d.Id == studentProgressDTO.Id);
            if (studentProgressEntity == null) return;

            studentProgressEntity.TotalLecturePoints = studentProgressDTO.TotalLecturePoints;
            studentProgressEntity.NeedsRetake = studentProgressDTO.NeedsRetake;
            studentProgressEntity.FinalGrade = studentProgressDTO.FinalGrade;
            studentProgressEntity.TotalPracticalPoints = studentProgressDTO.TotalPracticalPoints;
            studentProgressEntity.DisciplineDetailId = studentProgressDTO.DisciplineDetailId;
            studentProgressEntity.StudentId = studentProgressDTO.StudentId;
            studentProgressEntity.TotalPoints = studentProgressDTO.TotalPoints;

            _context.SaveChanges();
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
