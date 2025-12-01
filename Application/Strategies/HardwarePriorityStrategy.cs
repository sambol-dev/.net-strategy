using CaseStrategy.Domain.Interfaces;
using CaseStrategy.Domain.Models;

namespace CaseStrategy.Application.Strategies
{
    public class HardwarePriorityStrategy : IPriorityStrategy
    {
        public string DeterminePriority(SupportRequest request) => "Alta";
    }
}