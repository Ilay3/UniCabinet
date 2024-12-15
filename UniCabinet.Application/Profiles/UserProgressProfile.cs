using AutoMapper;
using UniCabinet.Core.DTOs.StudentManagement;
using UniCabinet.Core.DTOs.UserManagement;
using UniCabinet.Core.Models.ViewModel.User;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Application.Profiles
{
    public class UserProgressProfile : Profile
    {
        public UserProgressProfile()
        {
            CreateMap<StudentProgressEntity, StudentProgressDTO>().ReverseMap();    

        }
    }

}
