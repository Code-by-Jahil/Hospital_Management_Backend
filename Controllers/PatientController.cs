using Hospital_Management.DTO;
using Hospital_Management.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hospital_Management.Models;

namespace Hospital_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;

        public PatientController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Patient patient)
        {
            try
            {
                var result = await _patientRepository.AddPatientAsync(patient);
                return Ok(new { id = result.id, Message = result.Message });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(PatientDto patientDto)
        {
            try
            {
                var message = await _patientRepository.UpdatePatientAsync(patientDto);
                return Ok(new { Message = message });
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
                var message = await _patientRepository.DeletePatientAsync(id);
                return Ok(new { Message = message });
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
                var users = await _patientRepository.GetAllPatientAsync();
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
                var user = await _patientRepository.GetPatientByIdAsync(id);
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
