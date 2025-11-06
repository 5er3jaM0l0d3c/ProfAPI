using Dapper;
using ProfServer.Application.Interfaces;
using ProfServer.Infrastructure.DbContext;
using ProfServer.Models;

namespace ProfServer.Infrastructure.Repositories
{
    public class MachineRepository : IMachineRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        public MachineRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> AddMachineAsync(Machine machine)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                INSERT INTO ""Machine"" (""Address"", ""PaymentTypeId"", ""SerialNumber"", ""InventoryNumber"", ""ManufacturerId"", ""ManufactureDate"", 
                                      ""BeginExplotationDate"", ""InterverificationIntervalMonth"", ""ResourceHours"", ""ServiceTime"", 
                                      ""StatusId"", ""ManufactureCountryId"")
                VALUES (@Address, @PaymentTypeId, @SerialNumber, @InventoryNumber, @ManufacturerId, @ManufactureDate, 
                        @BeginExplotationDate, @InterverificationIntervalMonth, @ResourceHours, @ServiceTime, 
                        @StatusId, @ManufactureCountryId)
                RETURNING ""Id""";

            return await connection.ExecuteScalarAsync<int>(sql, machine);
        }

        public async Task<bool> DeleteMachineAsync(int id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"DELETE FROM ""Machine"" WHERE ""Id"" = @Id";

            return await connection.ExecuteAsync(sql, new { Id = id }).ContinueWith(t => t.Result > 0);
        }

        public async Task<Machine?> GetMachineByIdAsync(int id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"SELECT * FROM ""MachineView"" WHERE ""Id"" = @Id";

            return await connection.QuerySingleOrDefaultAsync<Machine>(sql, new { Id = id });
        }

        public async Task<IEnumerable<Machine>> GetMachinesAsync()
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"SELECT * FROM ""MachineView""";

            return await connection.QueryAsync<Machine>(sql);
        }

        public async Task<bool> UpdateMachineAsync(Machine machine)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                UPDATE ""Machine""
                SET ""Address"" = @Address,
                    ""PaymentTypeId"" = @PaymentTypeId,
                    ""SerialNumber"" = @SerialNumber,
                    ""InventoryNumber"" = @InventoryNumber,
                    ""ManufacturerId"" = @ManufacturerId,
                    ""ManufactureDate"" = @ManufactureDate,
                    ""BeginExplotationDate"" = @BeginExplotationDate,
                    ""InterverificationIntervalMonth"" = @InterverificationIntervalMonth,
                    ""ResourceHours"" = @ResourceHours,
                    ""ServiceTime"" = @ServiceTime,
                    ""StatusId"" = @StatusId,
                    ""ManufactureCountryId"" = @ManufactureCountryId,
                    ""InventarizationDate"" = @InventarizationDate
                WHERE ""Id"" = @Id";

            return await connection.ExecuteAsync(sql, machine).ContinueWith(t => t.Result > 0);
        }
    }
}
