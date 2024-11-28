using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.CourseManagement;
using UniCabinet.Core.DTOs.DepartmentManagmnet;
using UniCabinet.Core.DTOs.SpecializationManagement;
using UniCabinet.Infrastructure.Data;

namespace UniCabinet.Infrastructure.Implementations.Repository
{
    public class SpecializationRepositoryImpl : ISpecializationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SpecializationRepositoryImpl> _logger;
        private readonly IMapper _mapper;
        public SpecializationRepositoryImpl(
            ApplicationDbContext context, 
            ILogger<SpecializationRepositoryImpl> logger, 
            IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<SpecializationDTO>> GetAllSpecialization()
        {
            var specializationEntity = await _context.Specialties.ToListAsync();
            return _mapper.Map<List<SpecializationDTO>>(specializationEntity);
        }
    }
}
