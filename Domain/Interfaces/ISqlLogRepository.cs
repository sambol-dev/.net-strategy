namespace CaseStrategy.Domain.Interfaces
{
    public interface ISqlLogRepository
    {
        Task LogAsync(string log, Guid requestId);
    }
}