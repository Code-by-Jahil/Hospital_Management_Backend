using Hospital_Management.Models;

namespace Hospital_Management.Repositories
{
    public interface ILoginRepository
    {
        Task<Users> LoginAsync(string email);
    }
}
