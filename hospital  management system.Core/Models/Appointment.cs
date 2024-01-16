using hospital__management_system.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital__management_system.Core.Models
{
    public class Appointment:BaseModel
    {
        public DateTime AppointmentDate { get; set; }
        public AppointmentStatus Status { get; set; }
        public string DoctorName { get; set; }
        public string Description { get; set; }
        public Patient Patient { get; set; }
        
        [ForeignKey("Patient")]
        public int PatientId { get; set; }

    }
}
