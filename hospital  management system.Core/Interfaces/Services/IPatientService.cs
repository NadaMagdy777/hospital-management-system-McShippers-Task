using hospital__management_system.Core.Dtos.Account;
using hospital__management_system.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hospital__management_system.Core.Dtos.Patient;
using hospital__management_system.Core.Dtos.Appointment;

namespace hospital__management_system.Core.Interfaces.Services
{
    public interface IPatientService
    {
        Task<ResponseDTO> Edit(PatientEditDto patientDto, string id);

        Task<ResponseDTO> Delete(string PatientId);

        Task<ResponseDTO> AddPatient(UserRegisterDTO patientDto);
        Task<ResponseDTO> AddAppointmentToPatient(AddAppointmentDto appointmentDto);        
        Task<ResponseDTO> GetAllPatients();


    }
}
