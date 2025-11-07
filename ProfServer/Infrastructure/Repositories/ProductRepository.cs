using Dapper;
using ProfServer.Application.Interfaces;
using ProfServer.Infrastructure.DbContext;
using ProfServer.Models;

namespace ProfServer.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public ProductRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> CreateProductAsync(Product product)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                INSERT INTO ""Product"" (""Name"", ""Description"", ""Price"", ""MinimalStockQuantity"")
                VALUES (@Name, @Description, @Price, @MinimalStockQuantity)
                RETURNING ""Id""";

            return await connection.ExecuteScalarAsync<int>(sql, product);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                DELETE FROM ""Product"" 
                WHERE ""Id"" = @Id";

            return await connection.ExecuteAsync(sql, new { Id = id }).ContinueWith(t => t.Result > 0);
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                SELECT * FROM ""Product""
                WHERE ""Id"" = @Id";

            return await connection.QuerySingleOrDefaultAsync<Product>(sql, new { Id = id });
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                SELECT * FROM ""Product""";

            return await connection.QueryAsync<Product>(sql);
        }

        public async Task<IEnumerable<Product>> GetProductsByIdsAsync(IEnumerable<int> ids)
        {
            if (ids == null)
                return Enumerable.Empty<Product>();

            var idArray = ids as int[] ?? ids.ToArray();
            if (idArray.Length == 0)
                return Enumerable.Empty<Product>();

            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"SELECT * FROM ""Product"" WHERE ""Id"" = ANY(@Ids)";

            return await connection.QueryAsync<Product>(sql, new { Ids = idArray });
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                UPDATE ""Product""
                SET ""Name"" = @Name,
                    ""Description"" = @Description,
                    ""Price"" = @Price,
                    ""MinimalStockQuantity"" = @MinimalStockQuantity
                WHERE ""Id"" = @Id";

            return await connection.ExecuteAsync(sql, product).ContinueWith(t => t.Result > 0);
        }
    }
}
