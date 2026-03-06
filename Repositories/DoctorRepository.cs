using Dapper;
using Hospital_Management.DTO;
using Hospital_Management.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Hospital_Management.Repositories
{
    public class DoctorRepository : IDoctor
    {
        private readonly string _connectionString;

        public DoctorRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection CreateConnection() => new SqlConnection(_connectionString);
        public async Task<int> AddDoctorAsync(Doctor doctor)
        {
            try
            {
                using var connection = CreateConnection();
                var parameter = new DynamicParameters();


                parameter.Add("@name", doctor.Name);
                parameter.Add("@email", doctor.Email);
                parameter.Add("@address", doctor.Address);
                parameter.Add("@phone", doctor.Phone);
                parameter.Add("@gender", doctor.Gender);
                parameter.Add("@specialization", doctor.Specialization);

                parameter.Add("@new_user_id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await connection.ExecuteAsync("sp_InsertDoctor", parameter, commandType: CommandType.StoredProcedure);

                int newUserId = parameter.Get<int>("@new_user_id");

                return (newUserId);
            }
            catch (Exception ex)
            {

                throw;

            }
        }


        public async Task<string> UpdateDoctorAsync(Doctor doctor)
        {
            try
            {
                using var connection = CreateConnection();
                var parameter = new DynamicParameters();

                parameter.Add("@id", doctor.Id);
                parameter.Add("@name", doctor.Name);
                parameter.Add("@email", doctor.Email);
                parameter.Add("@address", doctor.Address);
                parameter.Add("@phone", doctor.Phone);
                parameter.Add("@gender", doctor.Gender);
                parameter.Add("@specialization", doctor.Specialization);

                await connection.ExecuteAsync("sp_UpdateDoctor", parameter, commandType: CommandType.StoredProcedure);
                return "Doctor updated successfully.";

            }
            catch (Exception ex) { throw; }
        }

        public async Task<string> DeleteDoctorAsync(int id)
        {
            try
            {
                using var connection = CreateConnection();
                var parameter = new DynamicParameters();

                parameter.Add("@id", id);

                await connection.ExecuteAsync("sp_DeleteDoctor", parameter, commandType: CommandType.StoredProcedure);
                return "Doctor deleted successfully.";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Doctor>> GetAllDoctorAsync()
        {
            try
            {
                using var connection = CreateConnection();
                var users = await connection.QueryAsync<Doctor>("sp_GetAllDoctor", commandType: CommandType.StoredProcedure);
                return users.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Doctor> GetDoctorByIdAsync(int id)
        {
            try
            {
                using var connection = CreateConnection();
                return await connection.QuerySingleOrDefaultAsync<Doctor>("sp_GetDoctorById", new { id }, commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

}

