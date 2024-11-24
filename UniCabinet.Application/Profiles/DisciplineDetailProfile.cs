using AutoMapper;
using UniCabinet.Core.DTOs.CourseManagement;
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
                .ForMember(dest => dest.SemesterNumber, opt => opt.MapFrom(src => src.Semester.Number))
                .ForMember(dest => dest.TeacherFirstName, opt => opt.MapFrom(src => src.Teacher.FirstName))
                .ForMember(dest => dest.TeacherLastName, opt => opt.MapFrom(src => src.Teacher.LastName))
                .ForMember(dest => dest.TeacherPatronymic, opt => opt.MapFrom(src => src.Teacher.Patronymic))
                .ForMember(dest => dest.CourseNumber, opt => opt.MapFrom(src => src.Course.Number));
        }
    }

}
