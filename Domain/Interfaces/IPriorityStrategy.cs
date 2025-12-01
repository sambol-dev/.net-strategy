using CaseStrategy.Domain.Models;

namespace CaseStrategy.Domain.Interfaces
{
    public interface IPriorityStrategy
    {
        string DeterminePriority(SupportRequest request);
    }
}