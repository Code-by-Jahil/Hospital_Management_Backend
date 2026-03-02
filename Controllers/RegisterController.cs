using Hospital_Management.DTO;
using Hospital_Management.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace Hospital_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public RegisterController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(RegisterDto userDto)
        {
            try
            {
                var result = await _userRepository.AddUserAsync(userDto);
                return Ok(new { id = result.id, Message = result.Message });
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Inter server error:{ex.Message}");
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(int id, RegisterDto userDto)
        {
            var message = await _userRepository.UpdateUserAsync(id, userDto);
            return Ok(new { Message = message });
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var message = await _userRepository.DeleteUserAsync(id);
            return Ok(new { Message = message });
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllUserAsync();
            return Ok(users);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetUsersById(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }
        }


    }
}
