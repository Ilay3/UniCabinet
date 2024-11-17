using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.UserManagement;

namespace UniCabinet.Application.UseCases.DepartmentUseCase;

public class GetUsersByDepartmentIdUseCase
{
    private readonly IDepartmentRepository _departmentRepository;
    public GetUsersByDepartmentIdUseCase(IDepartmentRepository departmentRepository) 
    {
        _departmentRepository = departmentRepository; 
    }

    public async Task<List<UserDTO>> ExecuteAsync(int depatrmnetId)
    {
        var result = await _departmentRepository.GetUsersByDepartmentAsync(depatrmnetId);
        return result;
    }
}
