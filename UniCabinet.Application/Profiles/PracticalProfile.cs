using AutoMapper;
using UniCabinet.Core.DTOs.PracticalManagement;
using UniCabinet.Core.DTOs.StudentManagement;
using UniCabinet.Core.Models.ViewModel.Practical;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Application.Profiles
{
    public class PracticalProfile : Profile
    {
        public PracticalProfile()
        {
            CreateMap<PracticalEntity, PracticalDTO>().ReverseMap();

            CreateMap<PracticalResultEntity, PracticalResultDTO>()
                .ForMember(dest => dest.StudentFirstName, opt => opt.MapFrom(src => src.Student.FirstName))
                .ForMember(dest => dest.StudentLastName, opt => opt.MapFrom(src => src.Student.LastName))
                .ForMember(dest => dest.StudentPatronymic, opt => opt.MapFrom(src => src.Student.Patronymic))
                .ForMember(dest => dest.PracticalNumber, opt => opt.MapFrom(src => src.Practical.PracticalName))
                .ReverseMap()
                .ForMember(dest => dest.Student, opt => opt.Ignore())
                .ForMember(dest => dest.Practical, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<StudentGradeDTO, PracticalResultEntity>()
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.StudentId))
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.Grade))
                .ForMember(dest => dest.PracticalId, opt => opt.Ignore())
                .ForMember(dest => dest.Student, opt => opt.Ignore())
                .ForMember(dest => dest.Practical, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<PracticalResultEntity, StudentGradeDTO>()
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.StudentId))
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => src.Grade))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Student.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Student.LastName))
                .ForMember(dest => dest.Patronymic, opt => opt.MapFrom(src => src.Student.Patronymic));

            CreateMap<PracticalDTO, PracticalAddVM>().ReverseMap();
            CreateMap<PracticalDTO, PracticalEditVM>().ReverseMap();

            CreateMap<PracticalDTO, PracticalListVM>();

            CreateMap<PracticalAttendanceDTO, PracticalAttendanceVM>().ReverseMap();
            CreateMap<StudentGradeDTO, StudentGradeVM>().ReverseMap();

        }
    }
}
