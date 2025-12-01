namespace CaseStrategy.Domain.Interfaces
{
    public interface ISQLiteRepository
    {
        Task LogAsync(string log, Guid requestId);
    }
}