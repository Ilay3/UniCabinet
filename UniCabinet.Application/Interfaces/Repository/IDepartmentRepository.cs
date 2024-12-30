using UniCabinet.Core.DTOs.DepartmentManagmnet;
using UniCabinet.Domain.Models;

namespace UniCabinet.Application.Interfaces.Repository;

public interface IDepartmentRepository
{
    Task<List<DepartmentDTO>> GetAllDepartment();
    Task<DepartmentEntity> GetDepartmentWithDisciplinesAndTeachersAsync(string headUserId);
    Task<List<DepartmentEntity>> GetDepartmentsWithUsersAsync();
    Task AddDepartmentAsync(DepartmentEntity department);
    Task UpdateDepartmentAsync(DepartmentEntity department);
    Task<DepartmentEntity> GetDepartmentByIdAsync(int departmentId);


}
