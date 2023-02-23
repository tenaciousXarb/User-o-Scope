using AppUser.DataAccess.AppData;
using AppUser.DataAccess.IRepositories;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace AppUser.DataAccess.Repositories
{
    public class UserRepository : EntityBaseRepository<User>, IUserRepository
    {
        #region fields
        private readonly UserProjectDbContext _context;
        private readonly SqlConnection _connection;
        #endregion


        #region ctor
        public UserRepository(UserProjectDbContext context, IConfiguration configuration) : base(context)
        {
            _context = context;
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        #endregion

        public async Task<List<User>?> GetAllSure()
        {
            var storedProcedure = "GetUsers";

            var parameters = new DynamicParameters();
            /*parameters.Add("@City", city);*/


            return (await _connection.QueryAsync<User>(sql: storedProcedure, param: parameters, commandType: CommandType.StoredProcedure)).AsList();
        }


        #region get by pagination async (DB)
        public async Task<List<User>> GetByPaginationAsync(int userPerPage, int pageNumber)
        {
            var storedProcedure = "GetPaginatedUsers";

            var parameters = new DynamicParameters();
            parameters.Add("UserPerPage", userPerPage);
            parameters.Add("PageNo", pageNumber);


            return (await _connection.QueryAsync<User>(sql: storedProcedure, param: parameters, commandType: CommandType.StoredProcedure)).AsList();
        }
        #endregion


        #region authenticate async (DB)
        public async Task<User?> AuthenticateAsync(string email, string password)
        {
            var storedProcedure = "ValidateUserCredentials";

            var parameters = new DynamicParameters();
            parameters.Add("Email", email);
            parameters.Add("Password", password);


            return await _connection.QueryFirstOrDefaultAsync<User>(sql: storedProcedure, param: parameters, commandType: CommandType.StoredProcedure);
        }
        #endregion


        #region validate email (DB)
        public async  Task<bool> UniqueEmail(string email, int id = 0)
        {
            var storedProcedure = "UniqueEmail";
            
            var parameters = new DynamicParameters();
            parameters.Add("Email", email);
            parameters.Add("Id", id);

            return !(await _connection.QueryAsync<User>(sql: storedProcedure, param: parameters, commandType: CommandType.StoredProcedure)).AsList().Any();
        }
        #endregion
    }
}
