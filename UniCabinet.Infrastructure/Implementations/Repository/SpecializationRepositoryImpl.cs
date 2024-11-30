using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UniCabinet.Application.Interfaces;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.SpecializationManagement;
using UniCabinet.Infrastructure.Data;

namespace UniCabinet.Infrastructure.Implementations.Repository
{
    public class SpecializationRepositoryImpl : ISpecializationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SpecializationRepositoryImpl> _logger;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public SpecializationRepositoryImpl(
            ApplicationDbContext context,
            ILogger<SpecializationRepositoryImpl> logger,
            IMapper mapper,
            IUserService userService)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<List<SpecializationDTO>> GetAllSpecialization()
        {
            var specializationEntity = await _context.Specialties.ToListAsync();
            return _mapper.Map<List<SpecializationDTO>>(specializationEntity);
        }
        public async Task<List<SpecializationDTO>> GetDataSpecializationAndTeacher()
        {

            var specializationEntity = await _context.Specialties
                .Include(t => t.Teachers)
                .ToListAsync();
            return _mapper.Map<List<SpecializationDTO>>(specializationEntity);
        }
    }
}
