using Dapper;
using ProfServer.Application.Interfaces;
using ProfServer.Infrastructure.DbContext;
using ProfServer.Models;
using System.Data;

namespace ProfServer.Infrastructure.Repositories
{
    public class Maintenance_WorkDescriptionRepository : IMaintenance_WorkDescriptionRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public Maintenance_WorkDescriptionRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> AddAsync(Maintenance_WorkDescription workDescription)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                INSERT INTO ""Maintenance_WorkDescription"" (""MaintenanceId"", ""WorkDescribtionId"")
                VALUES (@MaintenanceId, @WorkDescribtionId)
                RETURNING ""Id""";

            return await connection.ExecuteScalarAsync<int>(sql, workDescription);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                DELETE FROM ""Maintenance_WorkDescription"" 
                WHERE ""Id"" = @Id";

            return await connection.ExecuteAsync(sql, new { Id = id }).ContinueWith(t => t.Result > 0);
        }

        public async Task<IEnumerable<Maintenance_WorkDescription>> GetAllAsync()
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                SELECT * FROM ""Maintenance_WorkDescription""";

            return await connection.QueryAsync<Maintenance_WorkDescription>(sql);
        }

        public async Task<Maintenance_WorkDescription?> GetByIdAsync(int id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                SELECT * FROM ""Maintenance_WorkDescription""
                WHERE ""Id"" = @Id";

            return await connection.QuerySingleOrDefaultAsync<Maintenance_WorkDescription>(sql, new { Id = id });
        }

        public async Task<bool> UpdateAsync(Maintenance_WorkDescription workDescription)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                UPDATE ""Maintenance_WorkDescription""
                SET ""MaintenanceId"" = @MaintenanceId,
                    ""WorkDescribtionId"" = @WorkDescribtionId
                WHERE ""Id"" = @Id";

            return await connection.ExecuteAsync(sql, workDescription).ContinueWith(t => t.Result > 0);
        }
    }
}
