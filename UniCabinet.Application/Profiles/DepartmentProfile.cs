using AutoMapper;
using UniCabinet.Core.DTOs.DepartmentManagmnet;
using UniCabinet.Core.Models.ViewModel.Departmet;
using UniCabinet.Domain.Models;

namespace UniCabinet.Application.Profiles;

public class DepartmentProfile : Profile
{
    public DepartmentProfile()
    {
        CreateMap<DepartmentEntity, DepartmentDTO>().ReverseMap();
        CreateMap<DepartmentDTO, DepartmantVM>().ReverseMap();

        CreateMap<GetDepartmantAndUserDTO, GetDepartmantAndUserVM>().ReverseMap();
    }
}
