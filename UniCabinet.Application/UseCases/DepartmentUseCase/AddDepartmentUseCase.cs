using UniCabinet.Application.Interfaces.Repository;
using UniCabinet.Core.DTOs.DepartmentManagmnet;
using UniCabinet.Domain.Models;
using AutoMapper;

namespace UniCabinet.Application.UseCases.DepartmentUseCase;

public class AddDepartmentUseCase
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;

    public AddDepartmentUseCase(IDepartmentRepository departmentRepository, IMapper mapper)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
    }

    public async Task ExecuteAsync(DepartmentDTO departmentDTO)
    {
        if (departmentDTO == null)
            throw new ArgumentNullException(nameof(departmentDTO));

        var departmentEntity = _mapper.Map<DepartmentEntity>(departmentDTO);
        await _departmentRepository.AddDepartmentAsync(departmentEntity);
    }
}
