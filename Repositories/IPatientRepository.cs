using Hospital_Management.DTO;
using Hospital_Management.Models;

namespace Hospital_Management.Repositories
{
    public interface IPatientRepository
    {
        Task<(int id, string Message)> AddPatientAsync(Patient patient);
        Task<string> UpdatePatientAsync(PatientDto patientDto);
        Task<string> DeletePatientAsync(int id);
        Task<List<PatientDto>> GetAllPatientAsync();
        Task<PatientDto> GetPatientByIdAsync(int id);
    }
}
