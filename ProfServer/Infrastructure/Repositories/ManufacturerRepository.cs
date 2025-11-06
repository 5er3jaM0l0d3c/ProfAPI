using Dapper;
using ProfServer.Application.Interfaces;
using ProfServer.Infrastructure.DbContext;
using ProfServer.Models;

namespace ProfServer.Infrastructure.Repositories
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public ManufacturerRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> AddManufacturerAsync(Manufacturer manufacturer)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                INSERT INTO ""Manufacturer"" (""Name"")
                VALUES (@Name)
                RETURNING ""Id""";

            return await connection.ExecuteScalarAsync<int>(sql, manufacturer);
        }

        public async Task<bool> DeleteManufacturerAsync(int id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                DELETE FROM ""Manufacturer"" 
                WHERE ""Id"" = @Id";

            return await connection.ExecuteAsync(sql, new { Id = id }).ContinueWith(t => t.Result > 0);
        }

        public async Task<IEnumerable<Manufacturer>> GetAllManufacturersAsync()
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                SELECT * FROM ""Manufacturer""";

            return await connection.QueryAsync<Manufacturer>(sql);
        }

        public async Task<Manufacturer?> GetManufacturerByIdAsync(int id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"SELECT * FROM ""Manufacturer"" WHERE ""Id"" = @Id";

            return await connection.QuerySingleOrDefaultAsync<Manufacturer>(sql, new { Id = id });
        }

        public async Task<bool> UpdateManufacturerAsync(Manufacturer manufacturer)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                UPDATE ""Manufacturer""
                SET ""Name"" = @Name
                WHERE ""Id"" = @Id";

            return await connection.ExecuteAsync(sql, manufacturer).ContinueWith(t => t.Result > 0);
        }
    }
}
