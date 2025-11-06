using Dapper;
using ProfServer.Application.Interfaces;
using ProfServer.Infrastructure.DbContext;
using ProfServer.Models;

namespace ProfServer.Infrastructure.Repositories
{
    public class PaymentTypeRepository : IPaymentTypeRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public PaymentTypeRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> AddPaymentType(PaymentType paymentType)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                INSERT INTO ""PaymentType"" (""Name"")
                VALUES (@Name)
                RETURNING ""Id""";

            return await connection.ExecuteScalarAsync<int>(sql, paymentType);
        }

        public async Task<bool> DeletePaymentType(int id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                DELETE FROM ""PaymentType"" 
                WHERE ""Id"" = @Id";

            return await connection.ExecuteAsync(sql, new { Id = id }).ContinueWith(t => t.Result > 0);
        }

        public async Task<PaymentType?> GetPaymentTypeByIdAsync(int id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                SELECT * FROM ""PaymentType""
                WHERE ""Id"" = @Id";

            return await connection.QuerySingleOrDefaultAsync<PaymentType>(sql, new {Id = id});
        }

        public async Task<IEnumerable<PaymentType>> GetPaymentTypes()
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                SELECT * FROM ""PaymentType""";

            return await connection.QueryAsync<PaymentType>(sql);
        }

        public Task<bool> UpdatePaymentType(PaymentType paymentType)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                UPDATE ""PaymentType""
                SET ""Name"" = @Name
                WHERE ""Id"" = @Id";

            return connection.ExecuteAsync(sql, paymentType).ContinueWith(t => t.Result > 0);
        }
    }
}
