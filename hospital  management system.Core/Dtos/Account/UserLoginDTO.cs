using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital__management_system.Core.Dtos.Account
{
    public class UserLoginDTO
    {
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
