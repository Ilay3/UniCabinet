using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCabinet.Core.DTOs.SpecializationManagement;
using UniCabinet.Core.Models.ViewModel.Specialization;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Application.Profiles
{
    public class SpecializationProfile : Profile
    {
        public SpecializationProfile()
        {
            CreateMap<SpecialtyEntity, SpecializationDTO>()
                        .ForMember(dest => dest.Teacher, opt => opt.MapFrom(src => src.Teachers));
            CreateMap<SpecializationDTO, SpecializationVM>().ReverseMap();
        }
          
    }
}
