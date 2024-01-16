using hospital__management_system.Core.Dtos.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital__management_system.Core.Interfaces;
public interface ITokenGenerator
{
    Task<string> GenerateToken(AppUserTokenGeneratorDTO appUserDto);
    public string GenerateRandomToken();
}
