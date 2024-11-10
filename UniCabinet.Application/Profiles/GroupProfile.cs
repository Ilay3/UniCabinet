using AutoMapper;
using UniCabinet.Core.DTOs.Entites;
using UniCabinet.Core.Models.ViewModel.Group;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Application.Profiles
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<GroupEntity, GroupDTO>().ReverseMap();
            CreateMap<GroupDTO, GroupAddVM>()
             .ForSourceMember(src => src.Id, opt => opt.DoNotValidate())
             .ReverseMap();
            CreateMap<GroupDTO, GroupListVM>().ReverseMap();
            CreateMap<GroupDTO, GroupEditVM>().ReverseMap();

        }
    }
}
