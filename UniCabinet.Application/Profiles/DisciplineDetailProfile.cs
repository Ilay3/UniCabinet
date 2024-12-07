using AutoMapper;
using UniCabinet.Core.DTOs.CourseManagement;
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
                 .ReverseMap()
                 .ForMember(dest => dest.Course, opt => opt.Ignore()) 
                 .ForMember(dest => dest.Group, opt => opt.Ignore())
                 .ForMember(dest => dest.Semester, opt => opt.Ignore())
                 .ForMember(dest => dest.Discipline, opt => opt.Ignore());

            CreateMap<DisciplineDetailDTO, DisciplineDetailVM>().ReverseMap();
            CreateMap<DisciplineDetailDTO, DisciplineDetailInfoVM>().ReverseMap();
            CreateMap<DisciplineDetailFilterDTO, DisciplineDetailFilterVM>().ReverseMap();
            CreateMap<DisciplineDetailDTO, DisciplineDetailEditVM>()
                            .ForMember(dest => dest.Courses, opt => opt.Ignore())
                           .ForMember(dest => dest.Semesters, opt => opt.Ignore())
                           .ForMember(dest => dest.Groups, opt => opt.Ignore())
                           .ReverseMap();

            CreateMap<DisciplineDetailDTO, DisciplineDetailAddVM>()
                .ForMember(dest => dest.Courses, opt => opt.Ignore())
               .ForMember(dest => dest.Semesters, opt => opt.Ignore())
               .ForMember(dest => dest.Groups, opt => opt.Ignore())
               .ReverseMap();
        }
    }

}
