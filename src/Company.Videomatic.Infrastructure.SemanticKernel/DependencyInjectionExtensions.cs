using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Infrastructure.SemanticKernel;
using Company.Videomatic.Infrastructure.SemanticKernel.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddSemanticKernelDriver(this IServiceCollection services, IConfiguration configuration)
    {
        // IOptions
        services.Configure<SemanticKernelOptions>(configuration.GetSection("SemanticKernel"));

        // Services
        services.AddScoped<IKernel>(sp =>
        {
            var logFact = sp.GetRequiredService<ILoggerFactory>();

            var kernel = Kernel.Builder
                .WithLogger(logFact.CreateLogger<IKernel>())
                .Configure(cfg =>
                {
                    var options = sp.GetRequiredService<IOptions<SemanticKernelOptions>>().Value;

                    // IMPORTANT: if the model is not "gpt-3.5-turbo" we get errors in the HTTP calls later.
                    //cfg.AddOpenAIChatCompletionService("chat", options.ChatModel, options.ApiKey);

                    cfg.AddOpenAITextCompletionService(
                        serviceId: "textCompletion",
                        apiKey: options.ApiKey,
                        modelId: options.Model,
                        orgId: options.Organization ?? string.Empty,
                        overwrite: true);
                })
                // TODO: several other WithXXX to look into...
                //.WithMemoryStorage(new VolatileMemoryStore())
                .Build();

            return kernel;
        });

        services.AddScoped<IVideoAnalyzer, SemanticKernelVideoAnalyzer>();

        return services;
    }   
}