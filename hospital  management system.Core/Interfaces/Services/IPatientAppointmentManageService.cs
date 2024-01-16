using hospital__management_system.Core.Dtos.Patient;
using hospital__management_system.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital__management_system.Core.Interfaces.Services
{
    public interface IPatientAppointmentManageService

    {
        Task<ResponseDTO> GetAllAppointments(string PatientId);
        Task<ResponseDTO> CancelAppointment(int AppointmentId, string PatientID);
        Task<ResponseDTO> ConfirmAppointment(int AppointmentId, string PatientID);


    }
}
