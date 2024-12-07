using AutoMapper;
using UniCabinet.Core.DTOs.SpecializationManagement;
using UniCabinet.Core.DTOs.UserManagement;
using UniCabinet.Core.Models.ViewModel.Specialization;
using UniCabinet.Domain.Entities;

public class SpecializationProfile : Profile
{
    public SpecializationProfile()
    {
        CreateMap<SpecializationAddDTO, SpecialtyEntity>()
        .ForMember(dest => dest.Id, opt => opt.Ignore())
        .ForMember(dest => dest.Teachers, opt => opt.Ignore());
        CreateMap<SpecializationEditDTO, SpecialtyEntity>()
        .ForMember(dest => dest.Teachers, opt => opt.Ignore());
        CreateMap<SpecialtyEntity, SpecializationDTO>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.Teacher, opt => opt.MapFrom(src => src.Teachers));

        CreateMap<SpecialtyEntity, SpecializationListDTO>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        .ForMember(dest => dest.Teacher, opt => opt.MapFrom(src => src.Teachers.Select(t => new UserDTO
        {
            Id = t.Id,
            FirstName = t.FirstName,
            LastName = t.LastName,
            Email = t.Email
        }).ToList()));
        CreateMap<SpecializationDTO, SpecializationVM>().ReverseMap();
        CreateMap<SpecializationDTO, SpecializationEditVM>().ReverseMap();


        CreateMap<SpecializationDTO, SpecializationListDTO>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.Teacher, opt => opt.MapFrom(src => src.Teacher));

        CreateMap<SpecializationListDTO, SpecializationListVM>().ReverseMap();

        CreateMap<SpecializationAddVM, SpecializationAddDTO>().ReverseMap();

        CreateMap<SpecializationEditVM, SpecializationEditDTO>().ReverseMap();

        CreateMap<UserSpecialtiesAndDisciplinesDTO, UserSpecialtiesAndDisciplinesVM>()
            .ReverseMap();
    }
}
