using Dapper;
using Hospital_Management.DTO;
using Microsoft.Data.SqlClient;
using Swashbuckle.AspNetCore.Swagger;
using System.Data;
using System.Data.Common;
using Hospital_Management.Models;

namespace Hospital_Management.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public async Task<(int id, string Message)> AddUserAsync(RegisterDto userDto)
        {
            using var connection = CreateConnection();
            var parameter = new DynamicParameters();

            parameter.Add("@role_id", userDto.RoleId, DbType.Int32);
            parameter.Add("@username", userDto.Username);
            parameter.Add("@email", userDto.Email);
            parameter.Add("@password", BCrypt.Net.BCrypt.HashPassword(userDto.Password));
            parameter.Add("@address", userDto.Address);
            parameter.Add("@phone", userDto.Phone);
            parameter.Add("@gender", userDto.Gender);

            parameter.Add("@new_user_id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameter.Add("@message", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);

            await connection.ExecuteAsync("sp_InsertUser", parameter, commandType: CommandType.StoredProcedure);

            int newUserId = parameter.Get<int>("@new_user_id");
            string message = parameter.Get<string>("@message");

            return (newUserId, message);
        }

        public async Task<string> UpdateUserAsync(int id, RegisterDto userDto)
        {
            using var connection = CreateConnection();
            var parameter = new DynamicParameters();

            parameter.Add("@id", id);
            parameter.Add("@role_id", userDto.RoleId, DbType.Int32);
            parameter.Add("@username", userDto.Username);
            parameter.Add("@email", userDto.Email);
            parameter.Add("@password", userDto.Password);
            parameter.Add("@address", userDto.Address);
            parameter.Add("@phone", userDto.Phone);
            parameter.Add("@gender", userDto.Gender);

            parameter.Add("@message", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
            await connection.ExecuteAsync("sp_UpdateUser", parameter, commandType: CommandType.StoredProcedure);
            return parameter.Get<string>("@message");
        }

        public async Task<string> DeleteUserAsync(int id)
        {
            using var connection = CreateConnection();
            var parameter = new DynamicParameters();

            parameter.Add("@id", id);
            parameter.Add("@message", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);

            await connection.ExecuteAsync("sp_DeleteUser", parameter, commandType: CommandType.StoredProcedure);
            return parameter.Get<string>("@message");
        }

        public async Task<List<UsersDto>> GetAllUserAsync()
        {
            using var connection = CreateConnection();
            var users = await connection.QueryAsync<UsersDto>("sp_GetAllUsers", commandType: CommandType.StoredProcedure);
            return users.ToList();
        }

        public async Task<UsersDto> GetUserByIdAsync(int id)
        {
            using var connection = CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<UsersDto>("sp_GetUserById",new { id }, commandType: CommandType.StoredProcedure);
        }

        //public async Task<Users> Login(string email, string pass)
        //{
        //    using var connection = CreateConnection();
        //    return await connection.QuerySingleOrDefaultAsync<Users>("sp_GetUserById", new { id }, commandType: CommandType.StoredProcedure);
        //}
    }
}
