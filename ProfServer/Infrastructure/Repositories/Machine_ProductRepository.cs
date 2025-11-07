using Dapper;
using ProfServer.Application.Interfaces;
using ProfServer.Infrastructure.DbContext;
using ProfServer.Models;

namespace ProfServer.Infrastructure.Repositories
{
    public class Machine_ProductRepository : IMachine_ProductRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public Machine_ProductRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> CreateMachine_ProductAsync(Machine_Product machine_Product)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                INSERT INTO ""Machine_Product"" (""MachineId"", ""ProductId"", ""Quantity"")
                VALUES (@MachineId, @ProductId, @Quantity)
                RETURNING ""Id""";

            return await connection.ExecuteScalarAsync<int>(sql, machine_Product);
        }

        public async Task<bool> DeleteMachine_ProductAsync(int id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                DELETE FROM ""Machine_Product"" 
                WHERE ""Id"" = @Id";

            return await connection.ExecuteAsync(sql, new { Id = id }).ContinueWith(t => t.Result > 0);
        }

        public async Task<IEnumerable<int>> GetMachinesWhereProductAsync(int productId)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                SELECT ""MachineId"" FROM ""Machine_Product"" 
                WHERE ""ProductId"" = @ProductId";

            return await connection.QueryAsync<int>(sql, new { ProductId = productId });
        }

        public async Task<Machine_Product?> GetMachine_ProductByIdAsync(int id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                SELECT * FROM ""Machine_Product"" 
                WHERE ""Id"" = @Id";

            return await connection.QuerySingleOrDefaultAsync<Machine_Product>(sql, new { Id = id });
        }

        public async Task<Machine_Product?> GetMachine_ProductByMachineAndProductId(int machineId, int productId)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                SELECT * FROM ""Machine_Product"" 
                WHERE ""MachineId"" = @MachineId AND ""ProductId"" = @ProductId";

            return await connection.QuerySingleOrDefaultAsync<Machine_Product>(sql, new { MachineId = machineId, ProductId = productId });
        }

        public async Task<IEnumerable<int>> GetProductsInMachineAsync(int machineId)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                SELECT ""ProductId"" FROM ""Machine_Product"" 
                WHERE ""MachineId"" = @MachineId";

            return await connection.QueryAsync<int>(sql, new { MachineId = machineId });
        }

        public Task<bool> UpdateMachine_ProductAsync(Machine_Product machine_Product)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                UPDATE ""Machine_Product""
                SET ""MachineId"" = @MachineId,
                    ""ProductId"" = @ProductId,
                    ""Quantity"" = @Quantity
                WHERE ""Id"" = @Id";

            return connection.ExecuteAsync(sql, machine_Product).ContinueWith(t => t.Result > 0);
        }
    }
}
