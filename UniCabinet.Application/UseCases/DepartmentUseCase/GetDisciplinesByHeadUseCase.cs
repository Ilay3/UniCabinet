using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.CourseManagement;

namespace UniCabinet.Application.UseCases.DepartmentUseCase;

public class GetDisciplinesByHeadUseCase
{
    private readonly IDepartmentRepository _departmentRepository;
    public GetDisciplinesByHeadUseCase(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }
    public async Task<List<DisciplineDTO>> ExecuteAsync(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            throw new ArgumentException("User ID cannot be null or empty", nameof(userId));

        var department = await _departmentRepository.GetDepartmentByUserIdAsync(userId);
        if (department == null)
            throw new KeyNotFoundException($"Department not found for User ID: {userId}");

        return department.Discipline.ToList();
    }
}
