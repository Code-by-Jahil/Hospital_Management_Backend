using Hospital_Management.DTO;
using Hospital_Management.Models;

namespace Hospital_Management.Repositories
{
    public interface IAppointment
    {
        Task<int> AddAppointmentAsync(AppointmentDto appointmentdto);
        Task<int> UpdateAppointmentAsync(AppointmentUpdateDto appointment);
        Task<int> DeleteAppointmentAsync(Appointment appointment);
        Task<List<AppointmentViewDto>> GetAllAppointmentAsync();
        Task<AppointmentViewDto> GetAppointmentByIdAsync(int AppointmentId);
    }
}
