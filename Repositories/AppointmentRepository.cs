using Dapper;
using Hospital_Management.DTO;
using Hospital_Management.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection.Metadata;

namespace Hospital_Management.Repositories
{
    public class AppointmentRepository  : IAppointment
    {
        private readonly string _connectionString;

        public AppointmentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection CreateConnection() => new SqlConnection(_connectionString);


        public async Task<int> AddAppointmentAsync(AppointmentDto appointmentdto)
        {
            try
            {
                using var connection = CreateConnection();
                var parameter = new DynamicParameters();


                parameter.Add("@patient_id", appointmentdto.PatientId);
                parameter.Add("@doctor_id", appointmentdto.DoctorId);
                parameter.Add("@appointment_type", appointmentdto.AppointmentType);
                parameter.Add("@status", appointmentdto.Status);
                parameter.Add("@notes", appointmentdto.Notes);
                parameter.Add("@appointment_date", appointmentdto.AppointmentDate.ToString("dd-MM-yy"));
                parameter.Add("@appointment_time", appointmentdto.AppointmentTime.ToString(@"hh\:mm\:ss"));
                parameter.Add("@day_name", appointmentdto.DayName);


                parameter.Add("@new_appointment_id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await connection.ExecuteAsync("sp_InsertAppointment", parameter, commandType: CommandType.StoredProcedure);

                int appointmentId = parameter.Get<int>("@new_appointment_id");

                return (appointmentId);
            }
            catch (Exception ex)
            {

                throw;

            }
        }

        public async Task<int> UpdateAppointmentAsync(AppointmentUpdateDto appointment)
        {
            try
            {
                using var connection = CreateConnection();
                var parameter = new DynamicParameters();

                parameter.Add("@appointment_id", appointment.AppointmentId);
                parameter.Add("@appointment_type", appointment.AppointmentType);
                parameter.Add("@status", appointment.Status);
                parameter.Add("@notes", appointment.Notes);
                parameter.Add("@appointment_date", appointment.AppointmentDate.ToString("dd-MM-yy"));
                parameter.Add("@appointment_time", appointment.AppointmentTime.ToString(@"hh\:mm\:ss"));
                parameter.Add("@day_name", appointment.DayName);

                var update = await connection.ExecuteAsync("sp_UpdateAppointment", parameter, commandType: CommandType.StoredProcedure);
                return update;
            }
            catch (Exception ex) { throw; }

        }

        public async Task<int> DeleteAppointmentAsync(Appointment appointment)
        {
            try
            {
                using var connection = CreateConnection();
                var parameter = new DynamicParameters();

                parameter.Add("@appointment_id", appointment.AppointmentId);

                var Delete = await connection.ExecuteAsync("sp_DeleteAppointment", parameter, commandType: CommandType.StoredProcedure);
                return Delete;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<AppointmentViewDto>> GetAllAppointmentAsync()
        {
            try
            {
                using var connection = CreateConnection();
                var users = await connection.QueryAsync<AppointmentViewDto>("sp_GetAllAppointments", commandType: CommandType.StoredProcedure);
                return users.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<AppointmentViewDto> GetAppointmentByIdAsync(int AppointmentId)
        {
            try
            {
                using var connection = CreateConnection();
                var parameter = new DynamicParameters();

                parameter.Add("@appointment_id",AppointmentId);
                return await connection.QuerySingleOrDefaultAsync<AppointmentViewDto>("sp_GetAppointmentById",  parameter , commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
