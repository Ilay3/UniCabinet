using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.DepartmentManagmnet;
using UniCabinet.Core.DTOs.UserManagement;
using UniCabinet.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using UniCabinet.Core.DTOs.CourseManagement;

namespace UniCabinet.Application.UseCases.DepartmentUseCase;

public class GetDepartmentsWithUsersUseCase
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly UserManager<UserEntity> _userManager;
    private readonly IMapper _mapper;

    public GetDepartmentsWithUsersUseCase(
        IDepartmentRepository departmentRepository,
        UserManager<UserEntity> userManager,
        IMapper mapper)
    {
        _departmentRepository = departmentRepository;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<List<DepartmentsWithUsersDTO>> ExecuteAsync()
    {
        var departments = await _departmentRepository.GetDepartmentsWithUsersAsync();
        var departmentsDTO = new List<DepartmentsWithUsersDTO>();

        foreach (var department in departments)
        {
            var userDTOs = new List<UserDTO>();

            foreach (var user in department.User)
            {
                var roles = await _userManager.GetRolesAsync(user);

                var userDTO = _mapper.Map<UserDTO>(user);
                userDTO.Roles = roles.ToList();

                userDTOs.Add(userDTO);
            }

            var departmentDTO = new DepartmentsWithUsersDTO
            {
                Id = department.Id,
                DepartmentName = department.DepartmentName,
                Users = userDTOs,
                Discipline = _mapper.Map<List<DisciplineDTO>>(department.Discipline)
            };

            departmentsDTO.Add(departmentDTO);
        }

        return departmentsDTO;
    }
}