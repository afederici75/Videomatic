using Infrastructure.SemanticKernel;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.AI.Embeddings;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI.TextEmbedding;
using Microsoft.SemanticKernel.Memory;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddVideomaticSemanticKernel(this IServiceCollection services, IConfiguration configuration)
    {
        // IOptions
        services.Configure<SemanticKernelOptions>(configuration.GetSection("SemanticKernel"));

        // Services
        services.AddScoped<IKernel>(sp =>
        {
            var logFact = sp.GetRequiredService<ILoggerFactory>();
            var options = (sp.GetRequiredService<IOptions<SemanticKernelOptions>>()).Value;

            var kernel = Kernel.Builder
                .WithLogger(logFact.CreateLogger<IKernel>())
                .WithOpenAIChatCompletionService(
                    modelId: options.Model,
                    apiKey: options.ApiKey,
                    orgId: options.Organization,
                    serviceId: "chat",
                    alsoAsTextCompletion: true,
                    setAsDefault: false,
                    httpClient: null)
                .WithOpenAITextCompletionService(
                    modelId: options.Model,
                    apiKey: options.ApiKey,
                    orgId: options.Organization,
                    serviceId: "textCompletion",
                    setAsDefault: false,
                    httpClient: null)
                // TODO: switch to Weaviate asap
                .WithMemoryStorage(new VolatileMemoryStore())
                .Build();
                                

            return kernel;
        });

        services.AddTransient<IMemoryStore, VolatileMemoryStore>();
        services.AddTransient<ITextEmbeddingGeneration>((sp) =>
        {
            var options = (sp.GetRequiredService<IOptions<SemanticKernelOptions>>()).Value;

            var service = new OpenAITextEmbeddingGeneration(
                modelId: options.EmbeddingModel,
                apiKey: options.ApiKey,
                organization: options.Organization,
                httpClient: null,
                logger: null);

            return service;
        });

        services.AddTransient<ISemanticTextMemory, SemanticTextMemory>();

        return services;
    }   
}