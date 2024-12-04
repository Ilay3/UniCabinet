using AutoMapper;
using UniCabinet.Core.DTOs.DisciplineDetailManagment;
using UniCabinet.Core.Models.ViewModel.DisciplineDetail;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Application.Profiles
{
    public class DisciplineDetailProfile : Profile
    {
        public DisciplineDetailProfile()
        {
            CreateMap<DisciplineDetailEntity, DisciplineDetailDTO>()
                .ForMember(dest => dest.DisciplineName, opt => opt.MapFrom(src => src.Discipline.Name))
                .ForMember(dest => dest.GroupName, opt => opt.MapFrom(src => src.Group.Name))
                .ForMember(dest => dest.SemesterName, opt => opt.MapFrom(src => src.Semester.Number))
                .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => $"{src.Teacher.FirstName} {src.Teacher.LastName}"))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.Number))
                .ReverseMap();


            CreateMap<DisciplineDetailDTO, DisciplineDetailVM>().ReverseMap();
            CreateMap<DisciplineDetailDTO, DisciplineDetailInfoVM>().ReverseMap();
            CreateMap<DisciplineDetailFilterDTO, DisciplineDetailFilterVM>().ReverseMap();
        }
    }

}
