using hospital__management_system.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital__management_system.Core.Dtos.Appointment
{
    public class AddAppointmentDto
    {
        [Required]
        public DateTime AppointmentDate { get; set; }
        
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pennding;
        [Required]
        public string DoctorName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int PatientId { get; set; }
    }
}
