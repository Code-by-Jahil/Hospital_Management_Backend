using Hospital_Management.DTO;
using Hospital_Management.Models;

namespace Hospital_Management.Repositories
{
    
    public interface IDoctor
    {
    Task<int> AddDoctorAsync(Doctor doctor);
    Task<string> UpdateDoctorAsync(Doctor doctor);
    Task<string> DeleteDoctorAsync(int id);
    Task<List<Doctor>> GetAllDoctorAsync();
    Task<Doctor> GetDoctorByIdAsync(int id);
    
    }     
}
