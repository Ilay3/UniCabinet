using System.Runtime.CompilerServices;
using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.DepartmentManagmnet;

namespace UniCabinet.Application.UseCases.DepartmentUseCase
{
    public class GetDepartmnetDataUseCase
    {
        private readonly IDepartmentRepository _departmentRepository;
        public GetDepartmnetDataUseCase(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<List<DepartmentDTO>> ExecutreAsync()
        {
            var department = await _departmentRepository.GetAllDepartment();
            return department;

        }
    }
}
