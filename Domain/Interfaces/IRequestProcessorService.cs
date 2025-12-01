using CaseStrategy.Domain.Models;

namespace CaseStrategy.Domain.Interfaces
{
    public interface IRequestProcessorService
    {
        Task<ProcessedSupportRequest> ProcessAsync(SupportRequest request);
    }
}