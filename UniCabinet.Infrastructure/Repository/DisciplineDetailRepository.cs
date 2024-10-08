﻿using System;
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

        public DisciplineDetailDTO GetDisciplineDetailById(int id)
        {
            var disciplineDetailEntity = _context.DisciplineDetails.Find(id);
            if (disciplineDetailEntity == null) return null;

            return new DisciplineDetailDTO
            {
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
            var disciplineDetailEntity = _context.DisciplineDetails.ToList();

            return disciplineDetailEntity.Select(d => new DisciplineDetailDTO
            {
                Id = d.Id,
                GroupId = d.GroupId,
                SemesterId = d.SemesterId,
                DisciplineId = d.DisciplineId,
                TeacherId = d.TeacherId,
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
