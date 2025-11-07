using Dapper;
using ProfServer.Application.Interfaces;
using ProfServer.Infrastructure.DbContext;
using ProfServer.Models;

namespace ProfServer.Infrastructure.Repositories
{
    public class ProblemRepository : IProblemRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public ProblemRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> CreateProblem(Problem problem)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                INSERT INTO ""Problem"" (""Name"")
                VALUES (@Name)
                RETURNING ""Id""";

            return await connection.ExecuteScalarAsync<int>(sql, new { Name = problem.Name });
        }

        public async Task<bool> DeleteProblem(int id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                DELETE FROM ""Problem"" 
                WHERE ""Id"" = @Id";

            return await connection.ExecuteAsync(sql, new { Id = id }).ContinueWith(t => t.Result > 0);
        }

        public async Task<IEnumerable<Problem>> GetAllProblems()
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                SELECT * FROM ""Problem""";

            return await connection.QueryAsync<Problem>(sql);
        }

        public async Task<Problem?> GetProblemById(int id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                SELECT * FROM ""Problem""
                WHERE ""Id"" = @Id";

            return await connection.QuerySingleOrDefaultAsync<Problem>(sql, new { Id = id });
        }

        public async Task<bool> UpdateProblem(Problem problem)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            const string sql = @"
                UPDATE ""Problem""
                SET ""Name"" = @Name
                WHERE ""Id"" = @Id";

            return await connection.ExecuteAsync(sql, problem).ContinueWith(t => t.Result > 0);
        }
    }
}
