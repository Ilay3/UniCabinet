using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.CourseManagement;
using UniCabinet.Core.DTOs.DepartmentManagmnet;

namespace UniCabinet.Application.UseCases.DepartmentUseCase;

public class GetDisciplinesByHeadUseCase
{
    private readonly IDepartmentRepository _departmentRepository;
    public GetDisciplinesByHeadUseCase(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }
    public async Task<GetDepartmantAndUserDTO> ExecuteAsync(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentException("User ID cannot be null or empty", nameof(userId));

        var department = await _departmentRepository.GetDepartmentByUserIdAsync(userId);
        if (department == null)
            throw new KeyNotFoundException($"Department not found for User ID: {userId}");

        var users = await _departmentRepository.GetUsersByDepartmentAsync(department.Id);

        var rezult = new GetDepartmantAndUserDTO
        {
            Discipline = department.Discipline.ToList(),
            User = users.ToList()
        };
        
        return rezult;
    }
}
