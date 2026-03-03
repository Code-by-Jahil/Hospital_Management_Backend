using Hospital_Management.DTO;
using Hospital_Management.Models;

namespace Hospital_Management.Repositories
{
    public interface IUserRepository
    {
        Task<(int id, string Message)> AddUserAsync(RegisterDto userDto);
        Task<string> UpdateUserAsync( RegisterDto userDto);
        Task<string> DeleteUserAsync(int id);
        Task<List<UsersDto>> GetAllUserAsync();
        Task<UsersDto> GetUserByIdAsync(int id);

    }
}
