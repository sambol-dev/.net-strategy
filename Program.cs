using CaseStrategy.Application.Services;
using CaseStrategy.Application.Strategies;
using CaseStrategy.Domain.Interfaces;
using CaseStrategy.Domain.Models;
using CaseStrategy.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;

class Program
    {
        static async Task Main(string[] args)
        {
            using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(config =>
                {
                    config.AddJsonFile("appsettings.json");
                })
                .ConfigureServices((ctx, services) =>
                {
                    var cfg = ctx.Configuration;
                    var useMongo = cfg.GetValue<bool>("useMongoDb");

                    if (useMongo)
                    {
                        var client = new MongoClient(cfg.GetConnectionString("MongoDb"));
                        var db = client.GetDatabase("case_strategy");

                        services.AddSingleton<IMongoDatabase>(db);
                        services.AddScoped<IMongoRepository, MongoRepository>();
                    }
                    else
                    {
                        services.AddScoped<ISQLiteRepository, SQLiteRepository>();
                        services.AddScoped<IMongoRepository, MongoRepositoryFake>();
                    }

                    // Strategies
                    services.AddSingleton<HardwarePriorityStrategy>();
                    services.AddSingleton<SoftwarePriorityStrategy>();
                    services.AddSingleton<DefaultPriorityStrategy>();

                    // Service
                    services.AddScoped<IRequestProcessorService, RequestProcessorService>();
                })
                .Build();

            using var scope = host.Services.CreateScope();
            var svc = scope.ServiceProvider.GetRequiredService<IRequestProcessorService>();

            // Exemplo simples
            var request = new SupportRequest
            {
                UserName = "Samuel Bolsoni",
                Description = "Computador não liga",
                Category = "Hardware"
            };

            var processed = await svc.ProcessAsync(request);

            Console.WriteLine($"Processado: {processed.Id} | Prioridade: {processed.Priority}");
        }
    }