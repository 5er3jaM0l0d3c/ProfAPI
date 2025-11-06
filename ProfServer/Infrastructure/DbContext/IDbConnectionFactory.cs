using System.Data.Common;

namespace ProfServer.Infrastructure.DbContext
{
    public interface IDbConnectionFactory
    {
        DbConnection CreateConnection();
    }
}
