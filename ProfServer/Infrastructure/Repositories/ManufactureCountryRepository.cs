using Dapper;
using ProfServer.Application.Interfaces;
using ProfServer.Infrastructure.DbContext;
using ProfServer.Models;

namespace ProfServer.Infrastructure.Repositories
{
    public class ManufactureCountryRepository : IManufactureCountryRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public ManufactureCountryRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> AddManufactureCountry(ManufactureCountry manufactureCountry)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                INSERT INTO ""ManufactureCountry"" (""Name"")
                VALUES (@Name)
                RETURNING ""Id""";

            return await connection.ExecuteScalarAsync<int>(sql, new { Name = manufactureCountry.Name });
        }

        public async Task<bool> DeleteManufactureCountry(int id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                DELETE FROM ""ManufactureCountry"" 
                WHERE ""Id"" = @Id";

            return await connection.ExecuteAsync(sql, new { Id = id }).ContinueWith(t => t.Result > 0);
        }

        public async Task<IEnumerable<ManufactureCountry>> GetManufactureCountries()
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"SELECT * FROM ""ManufactureCountry""";

            return await connection.QueryAsync<ManufactureCountry>(sql);
        }

        public async Task<ManufactureCountry?> GetManufactureCountryById(int id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                SELECT * FROM ""ManufactureCountry"" 
                WHERE ""Id"" = @Id";

            return await connection.QuerySingleOrDefaultAsync<ManufactureCountry>(sql, new { Id = id });
        }

        public async Task<bool> UpdateManufactureCountry(ManufactureCountry manufactureCountry)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                UPDATE ""ManufactureCountry""
                SET ""Name"" = @Name
                WHERE ""Id"" = @Id";

            return await connection.ExecuteAsync(sql, manufactureCountry).ContinueWith(t => t.Result > 0);
        }
    }
}
