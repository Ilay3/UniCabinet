using AutoMapper;
using UniCabinet.Core.DTOs.Common;
using UniCabinet.Core.DTOs.SpecializationManagement;
using UniCabinet.Core.DTOs.StudentManagement;
using UniCabinet.Core.DTOs.UserManagement;
using UniCabinet.Core.Models;
using UniCabinet.Core.Models.ViewModel;
using UniCabinet.Core.Models.ViewModel.Common;
using UniCabinet.Core.Models.ViewModel.Departmet;
using UniCabinet.Core.Models.ViewModel.User;

namespace UniCabinet.Application.Profiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<UserDTO, UserVM>().ReverseMap();

            CreateMap<SelectListItemDTO, SelectListItemVM>().ReverseMap();

            CreateMap<StudentGroupDTO, StudentGroupVM>().ReverseMap();

            CreateMap<UserRolesDTO, UserRolesVM>().ReverseMap();

            CreateMap<UserGroupDTO, UserGroupVM>().ReverseMap();

            CreateMap<UserDetailDTO, UserDetailVM>().ReverseMap();

            CreateMap<PaginationModel, PaginationModel>().ReverseMap();

            CreateMap<SpecAndDepDTO, SpecAndDepVM>().ReverseMap();
        }
    }
}
