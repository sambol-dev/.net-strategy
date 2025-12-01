using CaseStrategy.Domain.Interfaces;
using CaseStrategy.Domain.Models;
using MongoDB.Driver;

namespace CaseStrategy.Infrastructure.Repositories
{
    public class MongoRepository : IMongoRepository
    {
        private readonly IMongoCollection<ProcessedSupportRequest> _col;
        public MongoRepository(IMongoDatabase db) => _col = db.GetCollection<ProcessedSupportRequest>("processed_support");
        public async Task SaveAsync(ProcessedSupportRequest processed) => await _col.InsertOneAsync(processed);
    }
}