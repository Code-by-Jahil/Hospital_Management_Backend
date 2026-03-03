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
                return StatusCode(500, $"Internal server error:{ex.Message}");
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(RegisterDto userDto)
        {
            try
            {
                var message = await _userRepository.UpdateUserAsync(userDto);
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
                    var message = await _userRepository.DeleteUserAsync(id);
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
                    var users = await _userRepository.GetAllUserAsync();
                    return Ok(users);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            [HttpGet]
            [Route("GetById")]
            public async Task<IActionResult> GetUsersById(int id)
            {
                try
                {
                    var user = await _userRepository.GetUserByIdAsync(id);
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
