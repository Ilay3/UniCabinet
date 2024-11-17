using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.DepartmentManagmnet;
using UniCabinet.Domain.Models;
using UniCabinet.Infrastructure.Data;

namespace UniCabinet.Infrastructure.Implementations.Repository;

public class DepartmentRepositoryImpl : IDepartmentRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    public DepartmentRepositoryImpl(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DepartmentDTO> GetAllDepartment()
    {
        var departmentEntity = await _context.Departments.ToListAsync();
        return _mapper.Map<DepartmentDTO>(departmentEntity);

    }
    public async Task<DepartmentDTO> GetDepartmentByUserIdAsync(string userId)
    {
        var departmentEntity = await _context.Departments
            .Include(d => d.Discipline)
            .FirstOrDefaultAsync(d => d.UserId == userId);

        return _mapper.Map<DepartmentDTO>(departmentEntity);
    }
}
