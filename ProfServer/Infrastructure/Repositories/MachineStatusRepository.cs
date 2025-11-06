using Dapper;
using ProfServer.Application.Interfaces;
using ProfServer.Infrastructure.DbContext;
using ProfServer.Models;

namespace ProfServer.Infrastructure.Repositories
{
    public class MachineStatusRepository : IMachineStatusRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public MachineStatusRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> AddMachineStatusAsync(MachineStatus status)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                INSERT INTO ""MachineStatus"" (""Name"")
                VALUES (@Name)
                RETURNING ""Id""";

            return await connection.ExecuteScalarAsync<int>(sql, new { Name = status.Name });
        }

        public Task<bool> DeleteMachineStatusAsync(int id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                DELETE FROM ""MachineStatus"" 
                WHERE ""Id"" = @Id";
            return connection.ExecuteAsync(sql, new { Id = id }).ContinueWith(t => t.Result > 0);
        }

        public async Task<MachineStatus?> GetMachineStatusByIdAsync(int id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                SELECT * FROM ""MachineStatus""
                WHERE ""Id"" = @Id";

            return await connection.QuerySingleOrDefaultAsync<MachineStatus>(sql, new { Id = id });
        }

        public async Task<IEnumerable<MachineStatus>> GetMachineStatusesAsync()
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                SELECT * FROM ""MachineStatus""";

            return await connection.QueryAsync<MachineStatus>(sql);
        }

        public Task<bool> UpdateMachineStatusAsync(MachineStatus status)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                UPDATE ""MachineStatus""
                SET ""Name"" = @Name
                WHERE ""Id"" = @Id";

            return connection.ExecuteAsync(sql, status).ContinueWith(t => t.Result > 0);
        }
    }
}
