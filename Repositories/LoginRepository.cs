using Azure.Core;
using Dapper;
using Hospital_Management.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Data;

namespace Hospital_Management.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly string _connectionString;

        public LoginRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Users> LoginAsync(string email)
        {
            using IDbConnection db = new SqlConnection(_connectionString);

            var parameters = new DynamicParameters();
            parameters.Add("@email", email);

            var data = await db.QueryFirstOrDefaultAsync<Users>("sp_LoginUser", parameters, commandType: CommandType.StoredProcedure);

            return data;
        }
    }
}
