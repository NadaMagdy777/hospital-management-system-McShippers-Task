using hospital__management_system.Core.Dtos.Patient;
using hospital__management_system.Core.Dtos;
using hospital__management_system.Core.Enums;
using hospital__management_system.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using hospital__management_system.EF.Services;

namespace hospital_management_system.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientAppointmentManageController : ControllerBase
    {

        private readonly IPatientAppointmentManageService _patientAppointmentManageService;

        public PatientAppointmentManageController(IPatientAppointmentManageService patientAppointmentManageService)
        {
            _patientAppointmentManageService = patientAppointmentManageService;
        }

        [Authorize]
        [HttpGet("GetMyAppointments")]
        public async Task<ActionResult<ResponseDTO>> GetAllAppointment()
        {
            if (!ModelState.IsValid) { return BadRequest(new ResponseDTO(ResponseStatusCode.InValidModel, false, ModelState)); };

            var userIdentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return Ok(await _patientAppointmentManageService.GetAllAppointments(userIdentifier));
        }

        [Authorize]
        [HttpPost("Confirm Appointment")]
        public async Task<ActionResult<ResponseDTO>> ConfirmAppointment(int AppointmentId)
        {
            if (!ModelState.IsValid) { return BadRequest(new ResponseDTO(ResponseStatusCode.InValidModel, false, ModelState)); };

            var userIdentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return Ok(await _patientAppointmentManageService.ConfirmAppointment(AppointmentId, userIdentifier));
        }
        [Authorize]
        [HttpPost("Cancel Appointment")]
        public async Task<ActionResult<ResponseDTO>> CancelAppointment(int AppointmentId)
        {
            if (!ModelState.IsValid) { return BadRequest(new ResponseDTO(ResponseStatusCode.InValidModel, false, ModelState)); };

            var userIdentifier = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return Ok(await _patientAppointmentManageService.CancelAppointment(AppointmentId, userIdentifier));
        }

    }
}
