using CaseStrategy.Domain.Interfaces;
using CaseStrategy.Domain.Models;

namespace CaseStrategy.Application.Strategies
{
public class DefaultPriorityStrategy : IPriorityStrategy
    {
        public string DeterminePriority(SupportRequest request) => "Baixa";
    }
}