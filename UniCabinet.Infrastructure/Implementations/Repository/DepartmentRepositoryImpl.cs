using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UniCabinet.Application.Interfaces;
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
    private readonly IUserService _userService;

    public DepartmentRepositoryImpl(ApplicationDbContext context, IMapper mapper, IUserService userService)

    {
        _context = context;
        _mapper = mapper;
        _userService = userService;
    }

    public async Task<List<DepartmentDTO>> GetAllDepartment()
    {
        var departmentEntity = await _context.Departments.ToListAsync();
        return _mapper.Map<List<DepartmentDTO>>(departmentEntity);

    }
    public async Task<DepartmentDTO> GetDepartmentByUserIdAsync(string userId)
    {
        var user = await _userService.GetUserByIdAsync(userId);

        var departmentEntity = await _context.Departments
            .Include(d=>d.Discipline)
            .FirstOrDefaultAsync(d=>d.Id == user.DepartmentId);

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

    public async Task<List<DepartmentEntity>> GetDepartmentsWithUsersAsync()
    {
        var departments = await _context.Departments
            .Include(d => d.User)
            .Include(d => d.Discipline)
                .ThenInclude(di => di.Specialty)
            .ToListAsync();

        return departments;
    }

    public async Task AddDepartmentAsync(DepartmentEntity department)
    {
        if (department == null)
            throw new ArgumentNullException(nameof(department));

        _context.Departments.Add(department);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateDepartmentAsync(DepartmentEntity department)
    {
        if (department == null)
            throw new ArgumentNullException(nameof(department));

        _context.Departments.Update(department);
        await _context.SaveChangesAsync();
    }

    public async Task<DepartmentEntity> GetDepartmentByIdAsync(int departmentId)
    {
        return await _context.Departments.FindAsync(departmentId);
    }


}
