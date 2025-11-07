using Dapper;
using ProfServer.Application.DTOs;
using ProfServer.Application.Interfaces;
using ProfServer.Infrastructure.DbContext;
using ProfServer.Models;

namespace ProfServer.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _dbConnectinFactory;

        public UserRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectinFactory = dbConnectionFactory;
        }

        public async Task<int> CreateUserAsync(User user)
        {
            using var connection = _dbConnectinFactory.CreateConnection();
            
            const string sql = @"
                INSERT INTO ""User"" (""Surname"", ""Name"", ""Patronymic"", ""Email"", ""Phone"", ""RoleId"", ""Login"", ""Password"")
                VALUES (@Surname, @Name, @Patronymic, @Email, @Phone, @RoleId, @Login, @Password)
                RETURNING ""Id""";

            return await connection.ExecuteScalarAsync<int>(sql, user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            using var connection = _dbConnectinFactory.CreateConnection();

            const string sql = @"
                DELETE FROM ""User""
                WHERE ""Id"" = @Id";

          return await connection.ExecuteAsync(sql, new { Id = id }).ContinueWith(r => r.Result > 0);
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            using var connection = _dbConnectinFactory.CreateConnection();

            const string sql = @"
                SELECT u.""Id"", u.""Surname"", u.""Name"", u.""Patronymic"", u.""Email"", u.""Phone"", u.""RoleId"", u.""Login""
                FROM ""User"" u
                WHERE u.""Id"" = @Id";

            return await connection.QuerySingleOrDefaultAsync<User>(sql, new { Id = id });
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            using var connection = _dbConnectinFactory.CreateConnection();

            const string sql = @"
                SELECT u.""Id"", u.""Surname"", u.""Name"", u.""Patronymic"", u.""Email"", u.""Phone"", u.""RoleId"", u.""Login""
                FROM ""User"" u";

            return await connection.QueryAsync<User>(sql);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            using var connection = _dbConnectinFactory.CreateConnection();

            const string sql = @"
                UPDATE ""User""
                SET ""Surname"" = @Surname,
                    ""Name"" = @Name,
                    ""Patronymic"" = @Patronymic,
                    ""Email"" = @Email,
                    ""Phone"" = @Phone,
                    ""RoleId"" = @RoleId,
                    ""Login"" = @Login,
                    ""Password"" = @Password
                WHERE ""Id"" = @Id";

            return await connection.ExecuteAsync(sql, user).ContinueWith(t => t.Result > 0);
        }

        public async Task<int> UserExistsAsync(string login, byte[] password)
        {
            using var connection = _dbConnectinFactory.CreateConnection();

            const string sql = @"
                SELECT ""Id"" FROM ""User""
                WHERE ""Login"" = @Login AND ""Password"" = @Password";

            return await connection.ExecuteScalarAsync<int>(sql, new { Login = login, Password = password } );
        }
    }
}
