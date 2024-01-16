using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital__management_system.Core.Dtos.Patient
{
    public class PatientGetDTO
    {
        public int ID { get; set; }

        public string AppUserID { get; set; }

        public string UserName { get; set; }


        public string Email { get; set; }


       
        

    }
}
