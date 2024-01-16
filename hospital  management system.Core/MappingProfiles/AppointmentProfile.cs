using AutoMapper;
using hospital__management_system.Core.Dtos.Appointment;
using hospital__management_system.Core.Dtos.Patient;
using hospital__management_system.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital__management_system.Core.MappingProfiles
{
    public class AppointmentProfile:Profile
    {
        public AppointmentProfile() {
            CreateMap<Appointment, AddAppointmentDto>()

                 .ReverseMap();

            CreateMap<Appointment, GetAppointmentDTO>()

                .ReverseMap();
        }
    }
}
