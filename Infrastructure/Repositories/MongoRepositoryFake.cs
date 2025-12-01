using CaseStrategy.Domain.Interfaces;
using CaseStrategy.Domain.Models;

namespace CaseStrategy.Infrastructure.Repositories
{
    public class MongoRepositoryFake : IMongoRepository
    {
        public Task SaveAsync(ProcessedSupportRequest request)
        {
            // Não faz nada (fallback)
            return Task.CompletedTask;
        }
    }

}