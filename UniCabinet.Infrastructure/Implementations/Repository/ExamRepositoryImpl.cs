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
    public class ExamRepositoryImpl : IExamRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ExamRepositoryImpl(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ExamDTO> GetExamByIdAsync(int id)
        {
            var examEntity = await _context.Exams.FindAsync(id);
            if (examEntity == null) return null;

            return _mapper.Map<ExamDTO>(examEntity);
        }

        public async Task<List<ExamDTO>> GetExamListByDisciplineDetailIdAsync(int disciplineDetailId)
        {
            var examList = await _context.Exams
                .Where(e => e.DisciplineDetailId == disciplineDetailId)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<ExamDTO>>(examList);
        }

        public async Task<List<ExamDTO>> GetAllExamsAsync()
        {
            var exams = await _context.Exams
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<ExamDTO>>(exams);
        }

        public async Task AddExamAsync(ExamDTO examDTO)
        {
            var examEntity = _mapper.Map<ExamEntity>(examDTO);
            await _context.Exams.AddAsync(examEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteExamAsync(int id)
        {
            var examEntity = await _context.Exams.FindAsync(id);
            if (examEntity != null)
            {
                _context.Exams.Remove(examEntity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateExamAsync(ExamDTO examDTO)
        {
            var examEntity = await _context.Exams.FirstOrDefaultAsync(e => e.Id == examDTO.Id);
            if (examEntity == null) return;

            _mapper.Map(examDTO, examEntity);
            _context.Exams.Update(examEntity);
            await _context.SaveChangesAsync();
        }
    }
}
