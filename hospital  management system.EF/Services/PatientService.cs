using AutoMapper;
using hospital__management_system.Core.Dtos;
using hospital__management_system.Core.Dtos.Account;
using hospital__management_system.Core.Dtos.Appointment;
using hospital__management_system.Core.Dtos.Patient;
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
    public class PatientService : IPatientService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountService _acountService;


        public PatientService(IMapper mapper, IUnitOfWork unitOfWork,IAccountService accountService)

        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _acountService=accountService;
            
        }
        public async Task<ResponseDTO> AddAppointmentToPatient(AddAppointmentDto appointmentDto)
        {
            Patient patient = await _unitOfWork.Repository<Patient>().FindAsync(p => p.ID == appointmentDto.PatientId);
            if (patient == null) { return new ResponseDTO(ResponseStatusCode.NotFound, true, null, "Patient Not Found"); }
            Appointment newAppointment=new Appointment();
            newAppointment= _mapper.Map<Appointment>(appointmentDto);
             _unitOfWork.Repository<Appointment>().Add(newAppointment);
            await _unitOfWork.SaveChangesAsync();

            return new ResponseDTO(ResponseStatusCode.Created, true, null, "Added Succsuflly");


        }

        public async Task<ResponseDTO> Delete(string PatientId)

        {
            Patient patient = await _unitOfWork.Repository<Patient>().FindAsync(s => s.ApplicationUser.Id == PatientId, new List<Expression<Func<Patient, object>>>
                      {
                        p => p.ApplicationUser,
                      }); ;
            if (patient == null)
            {
                return new ResponseDTO(ResponseStatusCode.NotFound, true, "Patient Not Found");
            }
            _unitOfWork.Repository<Patient>().Delete(patient);
             patient.ApplicationUser.IsDeleted= true;
            await _unitOfWork.SaveChangesAsync();
            return new ResponseDTO(ResponseStatusCode.Ok, true, "Successfully Deleted");
        }

        public async Task<ResponseDTO> Edit(PatientEditDto patientDto, string id)
        {
            Patient patient = await _unitOfWork.Repository<Patient>().FindAsync(p => p.ApplicationUserId == id &&p.IsDeleted==false,
                   new List<Expression<Func<Patient, object>>>
                      {
                        p => p.ApplicationUser,
                      });
        

            if (patient != null)
            {
                patient.Email = patientDto.Email;
                patient.UserName = patientDto.UserName;
                patient.ApplicationUser.Email = patientDto.Email;
                await _unitOfWork.SaveChangesAsync();
                return new ResponseDTO(ResponseStatusCode.Ok, true, null, "Edit Succsuflly");


            }
            return new ResponseDTO(ResponseStatusCode.NotFound, true, null , "Cant Edit User Not Found");
            ;
        }

        public async Task<ResponseDTO> GetAllPatients() {
            var patients = await _unitOfWork.Repository<Patient>().FindAllAsync(p=>p.IsDeleted==false,null);
            if (patients!= null)
            {
                List<PatientGetDTO> result = new List<PatientGetDTO>();
                result.AddRange(_mapper.Map<List<PatientGetDTO>>(patients));

                return new ResponseDTO(ResponseStatusCode.Ok, true, result);

            }
            return new ResponseDTO(ResponseStatusCode.Ok, true, null,"no patients found");


        }

        public async Task<ResponseDTO> AddPatient(UserRegisterDTO patientDto)
        {
           return await  _acountService.PatientRegister(patientDto);
        }
           


        

    }
}
