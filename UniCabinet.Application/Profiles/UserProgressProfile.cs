using AutoMapper;
using UniCabinet.Core.DTOs.StudentProgressManagment;
using UniCabinet.Core.Models.ViewModel.Student;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Application.Profiles
{
    public class UserProgressProfile : Profile
    {
        public UserProgressProfile()
        {
            CreateMap<StudentProgressEntity, StudentProgressDTO>()
                .ForMember(dest=>dest.DisciplineName, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<StudentProgressDTO, StudentProgressVM>()
              .ForMember(dest => dest.DisciplineName, opt => opt.MapFrom(src => src.DisciplineName)).ReverseMap()   ;

        }
    }

}
