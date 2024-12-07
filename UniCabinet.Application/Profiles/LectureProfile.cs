using AutoMapper;
using UniCabinet.Core.DTOs.LectureManagement;
using UniCabinet.Core.DTOs.StudentManagement;
using UniCabinet.Core.Models.ViewModel;
using UniCabinet.Core.Models.ViewModel.Lecture;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Application.Profiles
{
    public class LectureProfile : Profile
    {
        public LectureProfile()
        {
            CreateMap<LectureEntity, LectureDTO>().ReverseMap();
            CreateMap<LectureDTO, LectureAddVM>()
                .ForSourceMember(src => src.Id, opt => opt.DoNotValidate())
                .ReverseMap();
            CreateMap<LectureDTO, LectureEditVM>().ReverseMap();
            CreateMap<LectureDTO, LectureListVM>().ReverseMap();
            CreateMap<StudentAttendanceDTO, StudentAttendanceVM>().ReverseMap();

            CreateMap<LectureAttendanceDTO, LectureAttendanceVM>()
                .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.Students))
                .ReverseMap();


        }
    }
}
