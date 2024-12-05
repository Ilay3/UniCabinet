using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.PracticalManagement;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data;

namespace UniCabinet.Infrastructure.Implementations.Repository
{
    public class PracticalRepositoryImpl : IPracticalRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PracticalRepositoryImpl(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PracticalDTO> GetPracticalByIdAsync(int id)
        {
            var practicalEntity = await _context.Practicals.FindAsync(id);
            if (practicalEntity == null) return null;

            return _mapper.Map<PracticalDTO>(practicalEntity);
        }

        public async Task<List<PracticalDTO>> GetPracticalListByDisciplineDetailIdAsync(int id)
        {
            var practicalListEntity = await _context.Practicals
                .Where(p => p.DisciplineDetailId == id)
                .ToListAsync();

            return _mapper.Map<List<PracticalDTO>>(practicalListEntity);
        }

        public async Task<List<PracticalDTO>> GetAllPracticalsAsync()
        {
            var practicalEntities = await _context.Practicals.ToListAsync();

            return _mapper.Map<List<PracticalDTO>>(practicalEntities);
        }

        public async Task AddPracticalAsync(PracticalDTO practicalDTO)
        {
            var practicalEntity = _mapper.Map<PracticalEntity>(practicalDTO);

            await _context.Practicals.AddAsync(practicalEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePracticalAsync(int id)
        {
            var practicalEntity = await _context.Practicals.FindAsync(id);
            if (practicalEntity != null)
            {
                _context.Practicals.Remove(practicalEntity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdatePracticalAsync(PracticalDTO practicalDTO)
        {
            var practicalEntity = await _context.Practicals.FirstOrDefaultAsync(p => p.Id == practicalDTO.Id);
            if (practicalEntity == null) return;

            _mapper.Map(practicalDTO, practicalEntity);

            _context.Practicals.Update(practicalEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetPracticalCountByDisciplineDetailIdAsync(int disciplineDetailId)
        {
            return await _context.Practicals.CountAsync(p => p.DisciplineDetailId == disciplineDetailId);
        }
    }
}
