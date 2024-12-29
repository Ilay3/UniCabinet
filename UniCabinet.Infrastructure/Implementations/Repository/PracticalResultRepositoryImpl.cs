using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.PracticalManagement;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data;

namespace UniCabinet.Infrastructure.Implementations.Repository
{
    public class PracticalResultRepositoryImpl : IPracticalResultRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PracticalResultRepositoryImpl(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }



        public async Task AddOrUpdatePracticalResultAsync(PracticalResultDTO practicalResultDTO)
        {
            var existingResult = await _context.PracticalResults
                .FirstOrDefaultAsync(pr => pr.PracticalId == practicalResultDTO.PracticalId && pr.StudentId == practicalResultDTO.StudentId);

            if (existingResult != null)
            {
                _mapper.Map(practicalResultDTO, existingResult);
                _context.PracticalResults.Update(existingResult);
            }
            else
            {
                var practicalResultEntity = _mapper.Map<PracticalResultEntity>(practicalResultDTO);
                await _context.PracticalResults.AddAsync(practicalResultEntity);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<PracticalResultDTO>> GetPracticalResultsByPracticalIdAsync(int practicalId)
        {
            var practicalResultEntities = await _context.PracticalResults
                .Where(pr => pr.PracticalId == practicalId)
                .Include(pr => pr.Student)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<PracticalResultDTO>>(practicalResultEntities);
        }
    }
}
