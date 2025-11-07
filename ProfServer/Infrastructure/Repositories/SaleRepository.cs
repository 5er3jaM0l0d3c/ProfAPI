using Dapper;
using ProfServer.Application.Interfaces;
using ProfServer.Infrastructure.DbContext;
using ProfServer.Models;
using ProfServer.Models.Official;

namespace ProfServer.Infrastructure.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public SaleRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> CreateSaleAsync(Sale sale)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            int result;
            await connection.OpenAsync();
            var trans = await connection.BeginTransactionAsync();
            try
            {
                string sql = @"
                INSERT INTO ""Sale"" (""MachineId"", ""ProductId"", ""Quantity"", ""PaymentTypeId"")
                VALUES (@MachineId, @ProductId, @Quantity, @PaymentTypeId)
                RETURNING ""Id""";

                result = await connection.ExecuteScalarAsync<int>(sql, sale);

                sql = @"
                UPDATE ""Machine_Product""
                SET ""Quantity"" = ""Quantity"" - @Quantity
                WHERE ""MachineId"" = @MachineId AND ""ProductId"" = @ProductId";

                if(await connection.ExecuteAsync(sql, sale) <= 0)
                {
                    throw new NotFoundException(nameof(Machine_Product), $"MachineId: {sale.MachineId}, ProductId: {sale.ProductId}");
                }
                await trans.CommitAsync();
                await connection.CloseAsync();

            }
            catch (Exception)
            {
                await trans.RollbackAsync();
                throw;
            }

            return result;
        }

        public Task<bool> DeleteSaleAsync(int id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                DELETE FROM ""Sale""
                WHERE ""Id"" = @Id";

            return connection.ExecuteAsync(sql, new { Id = id }).ContinueWith(t => t.Result > 0);
        }

        public async Task<Sale?> GetSaleByIdAsync(int id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                SELECT * FROM ""SaleView""
                WHERE ""Id"" = @Id";

            return await connection.QuerySingleOrDefaultAsync<Sale>(sql, new { Id = id });
        }

        public async Task<Sale?> GetSaleByProductAndMachineIdAsync(int productId, int machineId)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                SELECT * FROM ""SaleView""
                WHERE ""ProductId"" = @ProductId AND ""MachineId"" = @MachineId";

            return await connection.QuerySingleOrDefaultAsync<Sale>(sql, new { ProductId = productId, MachineId = machineId });
        }

        public async Task<IEnumerable<Sale>> GetSalesFromMachineAsync(int machineId)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                SELECT * FROM ""SaleView""
                WHERE ""MachineId"" = @MachineId";

            return await connection.QueryAsync<Sale>(sql, new { MachineId = machineId });
        }

        public async Task<IEnumerable<Sale>> GetSalesFromProductAsync(int productId)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"SELECT * 
                                 From ""SaleView""
                                 WHERE ""ProductId"" = @ProductId";

            return await connection.QueryAsync<Sale>(sql, new { ProductId = productId });
        }

        public async Task<bool> UpdateSaleAsync(Sale sale)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                UPDATE ""Sale""
                SET ""MachineId"" = @MachineId,
                    ""ProductId"" = @ProductId,
                    ""Quantity"" = @Quantity,
                    ""Date"" = @Date,
                    ""PaymentTypeId"" = @PaymentTypeId
                WHERE ""Id"" = @Id";

            return await connection.ExecuteAsync(sql, sale).ContinueWith(t => t.Result > 0);
        }
    }
}
