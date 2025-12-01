using CaseStrategy.Domain.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CaseStrategy.Infrastructure.Repositories
{
    public class SqlLogRepository : ISqlLogRepository
    {
        private readonly string _connection;
        public SqlLogRepository(IConfiguration cfg) => _connection = cfg.GetConnectionString("SqlServer");

        public async Task LogAsync(string log, Guid requestId)
        {
            using var conn = new SqlConnection(_connection);
            const string sql = "INSERT INTO Logs (Id, RequestId, Message, CreatedAt) VALUES (@Id, @RequestId, @Message, @CreatedAt)";
            await conn.ExecuteAsync(sql, new { Id = Guid.NewGuid(), RequestId = requestId, Message = log, CreatedAt = DateTime.UtcNow });
        }
    }
}