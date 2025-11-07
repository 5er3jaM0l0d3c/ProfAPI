using Dapper;
using ProfServer.Application.Interfaces;
using ProfServer.Infrastructure.DbContext;
using ProfServer.Models;

namespace ProfServer.Infrastructure.Repositories
{
    public class WorkDescriptionRepository : IWorkDescriptionRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public WorkDescriptionRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> CreateWorkDescription(WorkDescription workDescription)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                INSERT INTO ""WorkDescription"" (""Name"")
                VALUES (@Name)
                RETURNING ""Id""";

            return await connection.ExecuteScalarAsync<int>(sql, new { Name = workDescription.Name });
        }

        public async Task<bool> DeleteWorkDescription(int id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                DELETE FROM ""WorkDescription"" 
                WHERE ""Id"" = @Id";

            return await connection.ExecuteAsync(sql, new { Id = id }).ContinueWith(t => t.Result > 0);
        }

        public async Task<IEnumerable<WorkDescription>> GetAllWorkDescriptions()
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                SELECT * FROM ""WorkDescription""";

            return await connection.QueryAsync<WorkDescription>(sql);
        }

        public async Task<WorkDescription?> GetWorkDescriptionById(int id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                SELECT * FROM ""WorkDescription""
                WHERE ""Id"" = @Id";

            return await connection.QuerySingleOrDefaultAsync<WorkDescription>(sql, new { Id = id });
        }

        public async Task<bool> UpdateWorkDescription(WorkDescription workDescription)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                UPDATE ""WorkDescription""
                SET ""Name"" = @Name
                WHERE ""Id"" = @Id";

            return await connection.ExecuteAsync(sql, new { Name = workDescription.Name, Id = workDescription.Id }) > 0;
        }
    }
}
