using Npgsql;
using System.Data.Common;

namespace ProfServer.Infrastructure.DbContext
{
    public class NpgsqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public NpgsqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
