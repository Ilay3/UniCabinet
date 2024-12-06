using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.ExamManagement;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

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
            var examResults = await _context.ExamResults
                .Where(er => er.ExamId == examId)
                .Include(er => er.Student)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<ExamResultDTO>>(examResults);
        }
    }
}
