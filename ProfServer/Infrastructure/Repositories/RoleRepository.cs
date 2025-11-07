using Dapper;
using ProfServer.Application.Interfaces;
using ProfServer.Infrastructure.DbContext;
using ProfServer.Models;

namespace ProfServer.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public RoleRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> CreateRoleAsync(Role role)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                INSERT INTO ""Role"" (""Name"")
                VALUES (@Name)
                RETURNING ""Id""";

            return await connection.ExecuteScalarAsync<int>(sql, role);
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            
            const string sql = @"
                DELETE FROM ""Role"" 
                WHERE ""Id"" = @Id";

            return await connection.ExecuteAsync(sql, new { Id = id }).ContinueWith(t => t.Result > 0);
        }

        public async Task<Role?> GetRoleByIdAsync(int id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                SELECT * FROM ""Role""
                WHERE ""Id"" = @Id";

            return await connection.QuerySingleOrDefaultAsync<Role>(sql, new { Id = id });
        }

        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"SELECT * FROM ""Role""";

            return await connection.QueryAsync<Role>(sql);
        }

        public async Task<bool> UpdateRoleAsync(Role role)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                UPDATE ""Role""
                SET ""Name"" = @Name
                WHERE ""Id"" = @Id";

            return await connection.ExecuteAsync(sql, role).ContinueWith(t => t.Result > 0);
        }
    }
}
