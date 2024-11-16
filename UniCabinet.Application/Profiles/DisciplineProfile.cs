using AutoMapper;
using UniCabinet.Core.DTOs.CourseManagement;
using UniCabinet.Core.Models.ViewModel.Discipline;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Application.Profiles
{
    public class DisciplineProfile : Profile
    {
        public DisciplineProfile()
        {
            CreateMap<DisciplineEntity, DisciplineDTO>().ReverseMap();
            CreateMap<DisciplineDTO, DisciplineAddVM>()
                .ForSourceMember(src => src.Id, opt => opt.DoNotValidate())
                .ReverseMap();
            CreateMap<DisciplineDTO, DisciplineEditVM>().ReverseMap();
            CreateMap<DisciplineDTO, DisciplineListVM>().ReverseMap();
        }
    }
}
