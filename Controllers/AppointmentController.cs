using Hospital_Management.DTO;
using Hospital_Management.Models;
using Hospital_Management.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointment _Appointment;

        public AppointmentController(IAppointment AppointmentRepository)
        {
            _Appointment = AppointmentRepository;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(AppointmentDto appointmentdto)
        {
            try
            {
                var result = await _Appointment.AddAppointmentAsync(appointmentdto);
                return Ok(new {message = "Appointment Created"});
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(AppointmentUpdateDto appointment)
        {
            try
            {
                var message = await _Appointment.UpdateAppointmentAsync(appointment);
                return Ok(new {mesage = "Appointment Update Successfull"});
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Appointment appointment)
        {
            try
            {
                var message = await _Appointment.DeleteAppointmentAsync(appointment);
                return Ok(new { Message = "Appointment Deleted" });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAppointment()
        {
            try
            {
                var users = await _Appointment.GetAllAppointmentAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetAppointmentById(int AppointmentId)
        {
            try
            {
                var user = await _Appointment.GetAppointmentByIdAsync(AppointmentId);
                if (user == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                throw;

            }
        }
    }
}
