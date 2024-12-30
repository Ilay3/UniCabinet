using AutoMapper;
using UniCabinet.Core.DTOs.UserManagement;
using UniCabinet.Core.Models.ViewModel.User;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Application.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserEntity, UserDTO>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore())
                .ForMember(dest => dest.SpecializationId, opt => opt.MapFrom(src => src.SpecialtyId));

            CreateMap<UserDTO, UserDetailVM>();
        }
    }



}
