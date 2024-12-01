using AutoMapper;
using UniCabinet.Core.DTOs.DepartmentManagmnet;
using UniCabinet.Core.Models.ViewModel.Department;
using UniCabinet.Core.Models.ViewModel.Departmet;
using UniCabinet.Domain.Models;

namespace UniCabinet.Application.Profiles;

public class DepartmentProfile : Profile
{
    public DepartmentProfile()
    {
        CreateMap<DepartmentEntity, DepartmentDTO>().ReverseMap();
        CreateMap<DepartmentDTO, DepartmantVM>().ReverseMap();

        CreateMap<DepartmentsWithUsersDTO, DepartmentsWithUsersVM>().ReverseMap();

        CreateMap<GetDepartmantAndUserDTO, GetDepartmantAndUserVM>().ReverseMap();

        CreateMap<DepartmentDTO, DepartmentAddEditVM>().ReverseMap();


    }
}
