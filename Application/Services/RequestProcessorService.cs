using CaseStrategy.Application.Strategies;
using CaseStrategy.Domain.Interfaces;
using CaseStrategy.Domain.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CaseStrategy.Application.Services
{
    public class RequestProcessorService : IRequestProcessorService
    {
        private readonly IMongoRepository _mongo;
        private readonly ISQLiteRepository _logger;
        private readonly IServiceProvider _provider;

        public RequestProcessorService(IMongoRepository mongo, ISQLiteRepository logger, IServiceProvider provider)
        {
            _mongo = mongo;
            _logger = logger;
            _provider = provider;
        }

        public async Task<ProcessedSupportRequest> ProcessAsync(SupportRequest request)
        {
            // De acordo com a categoria seleciona a estratégia
            IPriorityStrategy strategy = request.Category switch
            {
                "Hardware" => _provider.GetRequiredService<HardwarePriorityStrategy>(),
                "Software" => _provider.GetRequiredService<SoftwarePriorityStrategy>(),
                _ => _provider.GetRequiredService<DefaultPriorityStrategy>()
            };

            var priority = strategy.DeterminePriority(request);

            var processed = new ProcessedSupportRequest
            {
                RequestId = request.Id,
                Handler = "Sistema Automático",
                Priority = priority
            };

            await _mongo.SaveAsync(processed);
            await _logger.LogAsync($"Solicitação processada. Prioridade: {priority}", request.Id);

            return processed;
        }
    }
}