using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.DepartmentManagmnet;
using AutoMapper;

namespace UniCabinet.Application.UseCases.DepartmentUseCase;

public class GetDepartmentByIdUseCase
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;

    public GetDepartmentByIdUseCase(IDepartmentRepository departmentRepository, IMapper mapper)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
    }

    public async Task<DepartmentDTO> ExecuteAsync(int departmentId)
    {
        var departmentEntity = await _departmentRepository.GetDepartmentByIdAsync(departmentId);
        if (departmentEntity == null)
            return null;

        return _mapper.Map<DepartmentDTO>(departmentEntity);
    }
}
