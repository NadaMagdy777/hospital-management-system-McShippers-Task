using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital__management_system.Core.Models
{
    public class Patient:BaseModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
