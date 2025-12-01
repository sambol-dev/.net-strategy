using Microsoft.Data.Sqlite;
using Dapper;
using Microsoft.Extensions.Configuration;
using CaseStrategy.Domain.Interfaces;

namespace CaseStrategy.Infrastructure.Repositories
{
    public class SQLiteRepository : ISQLiteRepository
    {
        private readonly string _connection;

        public SQLiteRepository(IConfiguration cfg)
        {
            _connection = cfg.GetConnectionString("SQLite");
        }

        public async Task LogAsync(string log, Guid requestId)
        {
            await EnsureDatabaseAsync();

            using var conn = new SqliteConnection(_connection);

            const string sql = @"
                INSERT INTO Logs (Id, RequestId, Message, CreatedAt)
                VALUES (@Id, @RequestId, @Message, @CreatedAt);
            ";

            await conn.ExecuteAsync(sql, new
            {
                Id = Guid.NewGuid().ToString(),
                RequestId = requestId.ToString(),
                Message = log,
                CreatedAt = DateTime.UtcNow.ToString("o")
            });
        }

        private async Task EnsureDatabaseAsync()
        {
            using var conn = new SqliteConnection(_connection);
            await conn.ExecuteAsync(@"
                CREATE TABLE IF NOT EXISTS Logs (
                    Id TEXT PRIMARY KEY,
                    RequestId TEXT NOT NULL,
                    Message TEXT NOT NULL,
                    CreatedAt TEXT NOT NULL
                );
            ");
        }
    }   
}