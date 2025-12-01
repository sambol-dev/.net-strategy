using CaseStrategy.Domain.Interfaces;
using CaseStrategy.Domain.Models;

namespace CaseStrategy.Application.Strategies
{
public class SoftwarePriorityStrategy : IPriorityStrategy
    {
        public string DeterminePriority(SupportRequest request) => "Média";
    }
}