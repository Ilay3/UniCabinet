using AutoMapper;
using UniCabinet.Core.DTOs.ExamManagement;
using UniCabinet.Core.Models.ViewModel.Exam;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Application.Profiles
{
    public class ExamProfile : Profile
    {
        public ExamProfile()
        {
            CreateMap<ExamEntity, ExamDTO>().ReverseMap()
                .ForMember(dest => dest.DisciplineDetails, opt => opt.Ignore())
                .ForMember(dest => dest.ExamResults, opt => opt.Ignore());

            CreateMap<ExamResultEntity, ExamResultDTO>()
                .ForMember(dest => dest.StudentFirstName, opt => opt.MapFrom(src => src.Student.FirstName))
                .ForMember(dest => dest.StudentLastName, opt => opt.MapFrom(src => src.Student.LastName))
                .ForMember(dest => dest.StudentPatronymic, opt => opt.MapFrom(src => src.Student.Patronymic))
                .ReverseMap()
                .ForMember(dest => dest.Student, opt => opt.Ignore())
                .ForMember(dest => dest.Exam, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<ExamAddVM, ExamDTO>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<ExamEditVM, ExamDTO>().ReverseMap();

            CreateMap<ExamResultItemVM, ExamResultDTO>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ExamId, opt => opt.Ignore())
                .ForMember(dest => dest.StudentFirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.StudentLastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.StudentPatronymic, opt => opt.MapFrom(src => src.Patronymic));

            CreateMap<ExamResultDTO, ExamResultItemVM>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.StudentFirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.StudentLastName))
                .ForMember(dest => dest.Patronymic, opt => opt.MapFrom(src => src.StudentPatronymic));
        }
    }
}
