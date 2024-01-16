using AutoMapper;
using hospital__management_system.Core.Dtos.Account;
using hospital__management_system.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital__management_system.Core.MappingProfiles;
public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<UserRegisterDTO, ApplicationUser>();
        
        CreateMap<UserRegisterDTO, Patient>();

        CreateMap<UserRegisterDTO, BasicRegisterDTO>();
        
        CreateMap<BasicRegisterDTO, AppUserTokenGeneratorDTO>();
    
        
    }
}
