using AutoMapper;
using hospital__management_system.Core.Dtos;
using hospital__management_system.Core.Dtos.Appointment;
using hospital__management_system.Core.Enums;
using hospital__management_system.Core.Interfaces;
using hospital__management_system.Core.Interfaces.Services;
using hospital__management_system.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace hospital__management_system.EF.Services
{
    public class PatientAppointmentManageService : IPatientAppointmentManageService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PatientAppointmentManageService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public  async Task<ResponseDTO> CancelAppointment(int AppointmentId,string PatientID)
        {
            Appointment appointment = await _unitOfWork.Repository<Appointment>().FindAsync(a => a.ID == AppointmentId && a.Patient.ApplicationUserId == PatientID, new List<Expression<Func<Appointment, object>>>
                     {
                        a=>a.Patient
                     });
            if (appointment == null)
            {
                return new ResponseDTO(ResponseStatusCode.NotFound, true, null, "Appointment Not Found");

            }
            if (appointment.AppointmentDate.Date == DateTime.Today)
            {
                return new ResponseDTO(ResponseStatusCode.Ok, true, appointment.AppointmentDate.Day, "Not Allowed");

            }
            appointment.Status = AppointmentStatus.Canceled;
            await _unitOfWork.SaveChangesAsync();
            return new ResponseDTO(ResponseStatusCode.Ok, true, null, "Appointment Canceled Successfully");
        }

        public async Task<ResponseDTO> ConfirmAppointment(int AppointmentId,string PatientID)
        {
            Appointment appointment = await _unitOfWork.Repository<Appointment>().FindAsync(a => a.ID == AppointmentId &&a.Patient.ApplicationUserId== PatientID, new List<Expression<Func<Appointment, object>>>
                     {
                        a=>a.Patient
                     }); 
            if (appointment == null)
            {
                return new ResponseDTO(ResponseStatusCode.NotFound, true, null, "Appointment Not Found");

            }
            appointment.Status = AppointmentStatus.Confirmed;
            await _unitOfWork.SaveChangesAsync();
            return new ResponseDTO(ResponseStatusCode.Ok, true, null, "Appointment Confirmed Successfully");

        }

        public async Task<ResponseDTO> GetAllAppointments(string PatientId)
        {
            Patient patient = await _unitOfWork.Repository<Patient>().FindAsync(p => p.ApplicationUserId == PatientId ,
                  new List<Expression<Func<Patient, object>>>
                     {
                        p => p.Appointments,
                     });
            if(patient == null)
            {
                return new ResponseDTO(ResponseStatusCode.NotFound, true, null, "Patient Not Found");

            }
            if (patient?.Appointments.ToList().Count <1)
            {
                return new ResponseDTO(ResponseStatusCode.Ok, true, null, "NO Appointments For this Patient");
            }
            List<GetAppointmentDTO> patientAppointments= new List<GetAppointmentDTO>();
            patientAppointments.AddRange(_mapper.Map<List<GetAppointmentDTO>>(patient.Appointments));
            return new ResponseDTO(ResponseStatusCode.Ok, true, patientAppointments, "get appointments successfully");





        }
    }
}
