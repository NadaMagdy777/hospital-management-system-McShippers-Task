using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital__management_system.Core.Dtos.Patient
{
    public class PatientEditDto
    {
       
        [Required(ErrorMessage = "UserName is required")]

        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]

        public string Email { get; set; }

        


    }
}
