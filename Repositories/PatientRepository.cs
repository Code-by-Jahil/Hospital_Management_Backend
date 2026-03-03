using Dapper;
using Hospital_Management.DTO;
using Hospital_Management.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Hospital_Management.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly string _connectionString;

        public PatientRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public async Task<(int id, string Message)> AddPatientAsync(Patient patient)
        {
            try
            {
                using var connection = CreateConnection();
                var parameter = new DynamicParameters();

                
                parameter.Add("@name", patient.Name);
                parameter.Add("@email", patient.Email);
                parameter.Add("@password", BCrypt.Net.BCrypt.HashPassword(patient.Password));
                parameter.Add("@address", patient.Address);
                parameter.Add("@phone", patient.Phone);
                parameter.Add("@gender", patient.Gender);
                parameter.Add("@diseases", patient.Diseases);

                parameter.Add("@new_user_id", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameter.Add("@message", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);

                await connection.ExecuteAsync("sp_InsertPatient", parameter, commandType: CommandType.StoredProcedure);

                int newUserId = parameter.Get<int>("@new_user_id");
                string message = parameter.Get<string>("@message");

                return (newUserId, message);
            }
            catch (Exception ex)
            {

                throw;

            }
        }

        public async Task<string> UpdatePatientAsync(PatientDto patientDto)
        {
            try
            {
                using var connection = CreateConnection();
                var parameter = new DynamicParameters();

                parameter.Add("@id", patientDto.Id);
                parameter.Add("@name", patientDto.Name);
                parameter.Add("@email", patientDto.Email);
                parameter.Add("@address", patientDto.Address);
                parameter.Add("@phone", patientDto.Phone);
                parameter.Add("@gender", patientDto.Gender);
                parameter.Add("@diseases", patientDto.Diseases);

                parameter.Add("@message", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
                await connection.ExecuteAsync("sp_UpdatePatient", parameter, commandType: CommandType.StoredProcedure);
                return parameter.Get<string>("@message");
            }
            catch (Exception ex) { throw; }
        }

        public async Task<string> DeletePatientAsync(int id)
        {
            try
            {
                using var connection = CreateConnection();
                var parameter = new DynamicParameters();

                parameter.Add("@id", id);
                parameter.Add("@message", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);

                await connection.ExecuteAsync("sp_DeletePatient", parameter, commandType: CommandType.StoredProcedure);
                return parameter.Get<string>("@message");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<PatientDto>> GetAllPatientAsync()
        {
            try
            {
                using var connection = CreateConnection();
                var users = await connection.QueryAsync<PatientDto>("sp_GetAllPatient", commandType: CommandType.StoredProcedure);
                return users.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<PatientDto> GetPatientByIdAsync(int id)
        {
            try
            {
                using var connection = CreateConnection();
                return await connection.QuerySingleOrDefaultAsync<PatientDto>("sp_GetPatientById", new { id }, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
