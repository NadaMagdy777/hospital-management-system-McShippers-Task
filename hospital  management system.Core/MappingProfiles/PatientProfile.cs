using AutoMapper;
using hospital__management_system.Core.Dtos.Account;
using hospital__management_system.Core.Dtos.Patient;
using hospital__management_system.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital__management_system.Core.MappingProfiles
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<Patient, PatientEditDto>().ReverseMap();
                
            CreateMap<PatientGetDTO, Patient>()
                .ForMember(des => des.ApplicationUserId, opt => opt.MapFrom(src => src.AppUserID))
                .ReverseMap();

        }
    }
}
