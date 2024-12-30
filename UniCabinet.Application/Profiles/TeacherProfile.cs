using AutoMapper;
using UniCabinet.Core.DTOs.TeacherManagment;
using UniCabinet.Core.Models.ViewModel.Teacher;

namespace UniCabinet.Application.Profiles
{

    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<StudentGroupProgressDTO, StudentGroupProgressVM>().ReverseMap();
            CreateMap<GroupStudentsProgressPageDTO, GroupStudentsProgressPageVM>().ReverseMap();

        }
    }
}
