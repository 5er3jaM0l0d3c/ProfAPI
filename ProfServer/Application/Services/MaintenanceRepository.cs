using Dapper;
using ProfServer.Application.Interfaces;
using ProfServer.Infrastructure.DbContext;
using ProfServer.Models;
using ProfServer.Models.Official;

namespace ProfServer.Application.Services
{
    public class MaintenanceRepository : IMaintenanceRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public MaintenanceRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> CreateMaintanance(Maintenance maintenance)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.OpenAsync();
            var transaction = await connection.BeginTransactionAsync();

            try
            {
                string sql = @"INSERT INTO ""Maintenance""(""MachineId"", ""UserId"")
                                 VALUES (@MachineId, @UserId)
                                 RETURNING ""Id""";

                var result = await connection.ExecuteScalarAsync<int>(sql, maintenance);

                sql = @"INSERT INTO ""Maintenance_Problem""(""MaintenanceId"", ""ProblemId"")
                        SELECT @MaintenanceId, unnest(@ProblemIds)";

                var affectedRows = await connection.ExecuteAsync(sql, new
                {
                    MaintenanceId = result,
                    ProblemIds = maintenance.ProblemsIds.ToArray()
                });

                if (affectedRows != maintenance.ProblemsIds.Count())
                {
                    throw new ConflictException("Not all problems were inserted correctly. Try again.");
                }

                sql = @"INSERT INTO ""Maintenance_WorkDescription""(""MaintenanceId"", ""WorkDescriptionId"")
                        SELECT @MaintenanceId, unnest(@WorkDescriptionIds)";

                affectedRows = await connection.ExecuteAsync(sql, new
                {
                    MaintenanceId = result,
                    WorkDescriptionIds = maintenance.WorkDescriptionsIds.ToArray()
                });

                if (affectedRows != maintenance.WorkDescriptionsIds.Count())
                {
                    throw new ConflictException("Not all work descriptions were inserted correctly. Try again.");
                }
                await transaction.CommitAsync();
                await connection.CloseAsync();

                return result;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<bool> DeleteMaintenance(int id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                DELETE FROM ""Maintenance"" 
                WHERE ""Id"" = @Id";

            return await connection.ExecuteAsync(sql, new { Id = id }).ContinueWith(t => t.Result > 0);
        }

        public async Task<Maintenance?> GetMaintenanceById(int id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                SELECT * FROM ""Maintenance""
                WHERE ""Id"" = @Id";

            return await connection.QuerySingleOrDefaultAsync<Maintenance>(sql, new { Id = id });
        }

        public async Task<IEnumerable<Maintenance>> GetMaintenancesByMachineId(int machineId)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                SELECT * FROM ""Maintenance""
                WHERE ""MachineId"" = @MachineId";

            return await connection.QueryAsync<Maintenance>(sql, new { MachineId = machineId });
        }

        public async Task<IEnumerable<Maintenance>> GetMaintenancesByUserId(int userId)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                SELECT * FROM ""Maintenance""
                WHERE ""UserId"" = @UserId";

            return await connection.QueryAsync<Maintenance>(sql, new { UserId = userId });
        }

        public async Task<bool> UpdateMaintenance(Maintenance maintenance)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                UPDATE ""Maintenance""
                SET ""MachineId"" = @MachineId, ""UserId"" = @UserId
                WHERE ""Id"" = @Id";

            var affectedRows = await connection.ExecuteAsync(sql, maintenance);
            return affectedRows > 0;
        }
    }
}
