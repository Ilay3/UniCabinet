using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.DepartmentManagmnet;
using UniCabinet.Core.DTOs.UserManagement;
using UniCabinet.Domain.Entities;
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

    public async Task<List<UserDTO>> GetUsersByDepartmentAsync(int departmentId)
    {
        // Получаем кафедру с дисциплинами и специальностями
        var department = await _context.Departments
            .Include(d => d.Discipline)
            .ThenInclude(d => d.Specialty)
            .ThenInclude(s => s.Teachers)
            .FirstOrDefaultAsync(d => d.Id == departmentId);

        if (department == null)
            throw new KeyNotFoundException($"Department with ID {departmentId} not found");

        // Собираем пользователей из специальностей
        var users = department.Discipline
            .Where(d => d.Specialty != null)
            .SelectMany(d => d.Specialty.Teachers)
            .Distinct()
            .ToList();
        var userDTOs = new List<UserDTO>();

        foreach (var user in users)
        {

            userDTOs.Add(new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Patronymic = user.Patronymic,
                GroupName = user.Group != null ? user.Group.Name : "Без группы"
            });
        }


        return userDTOs;
    }
}
