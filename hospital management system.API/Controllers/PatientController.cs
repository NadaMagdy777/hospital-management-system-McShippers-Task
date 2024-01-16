using hospital__management_system.Core.Dtos.Account;
using hospital__management_system.Core.Dtos;
using hospital__management_system.Core.Enums;
using hospital__management_system.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using hospital__management_system.Core.Dtos.Patient;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using hospital__management_system.Core.Dtos.Appointment;
using hospital__management_system.Core.Constants;

namespace hospital_management_system.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [Authorize]
        [HttpPut("Edit")]
        public async Task<ActionResult<ResponseDTO>> Edit(PatientEditDto patientEditDto)
        {
            if (!ModelState.IsValid) { return BadRequest(new ResponseDTO(ResponseStatusCode.InValidModel, false, ModelState)); };

            var userIdentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return Ok(await _patientService.Edit(patientEditDto, userIdentifier));
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("Delete")]
        public async Task<ActionResult<ResponseDTO>> Delete(string PatientId)
        {
            if (!ModelState.IsValid) { return BadRequest(new ResponseDTO(ResponseStatusCode.InValidModel, false, ModelState)); };

            return Ok(await _patientService.Delete(PatientId));
        }
        [Authorize(Roles = Roles.Admin)]
        [HttpGet("GetAll")]
        public async Task<ActionResult<ResponseDTO>> GetAll()
        {
            if (!ModelState.IsValid) { return BadRequest(new ResponseDTO(ResponseStatusCode.InValidModel, false, ModelState)); };

            return Ok(await _patientService.GetAllPatients());
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("AddAppointment")]
        public async Task<ActionResult<ResponseDTO>> AddAppointmentToPatient(AddAppointmentDto appointmentDto)
        {
            if (!ModelState.IsValid) { return BadRequest(new ResponseDTO(ResponseStatusCode.InValidModel, false, ModelState)); };

            return Ok(await _patientService.AddAppointmentToPatient(appointmentDto));
        }

        [Authorize(Roles =Roles.Admin)]
        [HttpPost("AddPatient")]
        public async Task<ActionResult<ResponseDTO>> AddPatient(UserRegisterDTO patientDto)
        {
            if (!ModelState.IsValid) { return BadRequest(new ResponseDTO(ResponseStatusCode.InValidModel, false, ModelState)); };

            return Ok(await _patientService.AddPatient(patientDto));
        }


    }
}
