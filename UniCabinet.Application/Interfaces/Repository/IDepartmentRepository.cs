using UniCabinet.Core.DTOs.DepartmentManagmnet;
using UniCabinet.Core.DTOs.UserManagement;
using UniCabinet.Domain.Models;

namespace UniCabinet.Application.Interfaces.Repository;

public interface IDepartmentRepository
{
    Task<DepartmentDTO> GetAllDepartment();
    Task<DepartmentDTO> GetDepartmentByUserIdAsync(string userId);
    Task<List<UserDTO>> GetUsersByDepartmentAsync(int departmentId);

}
