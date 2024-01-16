using hospital__management_system.Core.Constants;
using hospital__management_system.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital__management_system.Core.Dtos.Account;
public class BasicRegisterDTO
{
    public ApplicationUser AppUser { get; set; }
    public BaseModel User { get; set; }
    public string Password { get; set; }
    public string UserName { get; set; }

}
