using Hospital_Management.DTO;
using Hospital_Management.Models;
using Hospital_Management.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {

        private readonly IDoctor _doctor;

        public DoctorController(IDoctor DoctorRepository)
        {
            _doctor = DoctorRepository;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Doctor doctor)
        {
            try
            {
                var result = await _doctor.AddDoctorAsync(doctor);
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(Doctor doctor)
        {
            try
            {
                var message = await _doctor.UpdateDoctorAsync(doctor);
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var message = await _doctor.DeleteDoctorAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _doctor.GetAllDoctorAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            try
            {
                var user = await _doctor.GetDoctorByIdAsync(id);
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

