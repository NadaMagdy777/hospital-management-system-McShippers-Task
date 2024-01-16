using hospital__management_system.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital__management_system.Core.Dtos.Appointment
{
    public class GetAppointmentDTO
    {
        public int ID { get; set; }

        public DateTime AppointmentDate { get; set; }

        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pennding;
        public string DoctorName { get; set; }
        public string Description { get; set; }
        
    }
}
