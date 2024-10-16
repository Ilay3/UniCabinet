using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Domain.DTO;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data;

namespace UniCabinet.Infrastructure.Repository
{
    public class DisciplineDetailRepository : IDisciplineDetailRepository
    {
        private readonly ApplicationDbContext _context;
        public DisciplineDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DisciplineDetailDTO> GetDisciplineDetailById(int id)
        {
            var disciplineDetailEntity = await _context.DisciplineDetails.FindAsync(id);
            if (disciplineDetailEntity == null) return null;

            return new DisciplineDetailDTO
            {
                CourseId = disciplineDetailEntity.CourseId,
                SemesterId = disciplineDetailEntity.SemesterId,
                GroupId = disciplineDetailEntity.GroupId,
                TeacherId = disciplineDetailEntity.TeacherId,
                DisciplineId = disciplineDetailEntity.DisciplineId,
                SubExamCount = disciplineDetailEntity.SubExamCount,
                LectureCount = disciplineDetailEntity.LectureCount,
                ExamCount = disciplineDetailEntity.ExamCount,
                MinLecturesRequired = disciplineDetailEntity.MinLecturesRequired,
                MinPracticalsRequired = disciplineDetailEntity.MinPracticalsRequired,
                AutoExamThreshold = disciplineDetailEntity.AutoExamThreshold,
                PassCount = disciplineDetailEntity.PassCount,
                PracticalCount = disciplineDetailEntity.PracticalCount,
            };
        }

        public List<DisciplineDetailDTO> GetAllDisciplineDetails()
        {
            var disciplineDetailEntity = _context.DisciplineDetails
                .Include(dd => dd.Group)
                .Include(dd => dd.Course)
                .Include(dd => dd.Semester)
                .Include(dd => dd.Discipline)
                .Include(dd => dd.Teacher);

            return disciplineDetailEntity.Select(d => new DisciplineDetailDTO
            {
                Id = d.Id,
                GroupId = d.GroupId,
                GroupName = d.Group.Name,
                SemesterId = d.SemesterId,
                SemesterNumber = d.Semester.Number,
                CourseId = d.CourseId,
                CourseNumber = d.Course.Number,
                DisciplineId = d.DisciplineId,
                DisciplineName = d.Discipline.Name,
                TeacherId = d.TeacherId,
                TeacherFirstName = d.Teacher.FirstName,
                TeacherLastName = d.Teacher.LastName,
                TeacherPatronymic = d.Teacher.Patronymic,
                SubExamCount = d.SubExamCount,
                PracticalCount = d.PracticalCount,
                PassCount = d.PassCount,
                MinPracticalsRequired = d.MinPracticalsRequired,
                MinLecturesRequired = d.MinLecturesRequired,
                LectureCount = d.LectureCount,
                ExamCount = d.ExamCount,
                AutoExamThreshold = d.AutoExamThreshold,
            }).ToList();
        }

        public async Task AddDisciplineDetailAsync(DisciplineDetailDTO disciplineDetailDTO)
        {
            var disciplineDetailEntity = new DisciplineDetail
            {
                TeacherId = disciplineDetailDTO.TeacherId,
                SemesterId = disciplineDetailDTO.SemesterId,
                CourseId = disciplineDetailDTO.CourseId,
                DisciplineId = disciplineDetailDTO.DisciplineId,
                GroupId = disciplineDetailDTO.GroupId,
                SubExamCount = disciplineDetailDTO.SubExamCount,
                PracticalCount = disciplineDetailDTO.PracticalCount,
                PassCount = disciplineDetailDTO.PassCount,
                LectureCount = disciplineDetailDTO.LectureCount,
                MinLecturesRequired = disciplineDetailDTO.MinLecturesRequired,
                MinPracticalsRequired = disciplineDetailDTO.MinPracticalsRequired,
                ExamCount = disciplineDetailDTO.ExamCount,
                AutoExamThreshold = disciplineDetailDTO.AutoExamThreshold,
            };

            await _context.DisciplineDetails.AddAsync(disciplineDetailEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDisciplineDetail(int id)
        {
            var disciplineDetailEntity = await _context.DisciplineDetails.FindAsync(id);
            if (disciplineDetailEntity != null)
            {
                _context.DisciplineDetails.Remove(disciplineDetailEntity);
                await _context.SaveChangesAsync();
            }
        }

        public void UpdateDisciplineDetail(DisciplineDetailDTO disciplineDetailDTO)
        {
            var disciplineDetailEntity = _context.DisciplineDetails.FirstOrDefault(d => d.Id == disciplineDetailDTO.Id);
            if (disciplineDetailEntity == null) return;

            disciplineDetailEntity.DisciplineId = disciplineDetailDTO.DisciplineId;
            disciplineDetailEntity.GroupId = disciplineDetailDTO.GroupId;
            disciplineDetailEntity.SemesterId = disciplineDetailDTO.SemesterId;
            disciplineDetailEntity.CourseId = disciplineDetailDTO.CourseId;
            disciplineDetailEntity.TeacherId = disciplineDetailDTO.TeacherId;
            disciplineDetailEntity.AutoExamThreshold = disciplineDetailDTO.AutoExamThreshold;
            disciplineDetailEntity.MinLecturesRequired = disciplineDetailDTO.MinLecturesRequired;
            disciplineDetailEntity.MinPracticalsRequired = disciplineDetailDTO.MinPracticalsRequired;
            disciplineDetailEntity.ExamCount = disciplineDetailDTO.ExamCount;
            disciplineDetailEntity.LectureCount = disciplineDetailDTO.LectureCount;
            disciplineDetailEntity.SubExamCount = disciplineDetailDTO.SubExamCount;
            disciplineDetailEntity.PracticalCount = disciplineDetailDTO.PracticalCount;
            disciplineDetailEntity.PassCount = disciplineDetailDTO.PassCount;

            _context.SaveChanges();
        }

    }
}
