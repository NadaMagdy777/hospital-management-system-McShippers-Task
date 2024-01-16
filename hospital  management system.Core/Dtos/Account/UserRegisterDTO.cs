using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital__management_system.Core.Dtos.Account
{
    public class UserRegisterDTO
    {
        [Required(ErrorMessage = "UserName is required")]

        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required"),EmailAddress]
        public string Email { get; set; }


        [RegularExpression("^(Patient|Doctor|Admin)$", ErrorMessage = "UserType should be either 'Patient' , 'Doctor' Or 'Admin'.")]
        public string UserType { get; set; }
       

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
