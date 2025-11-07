using Dapper;
using ProfServer.Application.Interfaces;
using ProfServer.Infrastructure.DbContext;
using ProfServer.Models;

namespace ProfServer.Infrastructure.Repositories
{
    public class Maintenance_ProblemRepository : IMaintenance_ProblemRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public Maintenance_ProblemRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> AddAsync(Maintenance_Problem problem)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                INSERT INTO ""Maintenance_Problem"" (""MaintenanceId"", ""ProblemId"")
                VALUES (@MaintenanceId, @ProblemId)";

            return await connection.ExecuteAsync(sql, problem);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                DELETE FROM ""Maintenance_Problem"" 
                WHERE ""Id"" = @Id";

            return await connection.ExecuteAsync(sql, new { Id = id }) > 0;
        }

        public async Task<IEnumerable<Maintenance_Problem>> GetAllAsync()
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                SELECT * FROM ""Maintenance_Problem""";

            return await connection.QueryAsync<Maintenance_Problem>(sql);
        }

        public async Task<Maintenance_Problem?> GetByIdAsync(int id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                SELECT * FROM ""Maintenance_Problem""
                WHERE ""Id"" = @Id";

            return await connection.QuerySingleOrDefaultAsync<Maintenance_Problem>(sql, new { Id = id });
        }

        public Task<bool> UpdateAsync(Maintenance_Problem problem)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                UPDATE ""Maintenance_Problem""
                SET ""MaintenanceId"" = @MaintenanceId,
                    ""ProblemId"" = @ProblemId
                WHERE ""Id"" = @Id";

            return connection.ExecuteAsync(sql, problem).ContinueWith(t => t.Result > 0);
        }
    }
}
