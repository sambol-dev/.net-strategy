using CaseStrategy.Domain.Models;

namespace CaseStrategy.Domain.Interfaces
{
    public interface IMongoRepository
    {
        Task SaveAsync(ProcessedSupportRequest processed);
    }
}