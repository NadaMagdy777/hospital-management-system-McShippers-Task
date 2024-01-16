using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital__management_system.Core.Dtos.Account
{
    public class ForgetPasswordRequestDTO
    {
        [Required,EmailAddress]
        public string Email { get; set; }
    }
}
