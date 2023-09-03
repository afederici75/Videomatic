using Application.Abstractions;
using Infrastructure.SemanticKernel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.AI.Embeddings;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI.TextEmbedding;
using Microsoft.SemanticKernel.Connectors.Memory.Redis;
using Microsoft.SemanticKernel.Memory;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddVideomaticSemanticKernel(this IServiceCollection services, IConfiguration configuration)
    {
        // IOptions
        services.Configure<SemanticKernelOptions>(configuration.GetSection("SemanticKernel"));

        // Services
        services.AddTransient<IKernel>(sp =>
        {
            var logFact = sp.GetRequiredService<ILoggerFactory>();
            var options = (sp.GetRequiredService<IOptions<SemanticKernelOptions>>()).Value;

            // TODO: make configurable from appsettings.json
            var store = new RedisMemoryStore(options.MemoryStoreEndpoint!);
            
            // TODO: fix!!!            
            //var store = new WeaviateMemoryStore(
            //    endpoint: options.MemoryStoreEndpoint!,
            //    apiKey: options.MemoryStoreApiKey);
            //if (store.DoesCollectionExistAsync("Videos").Result == false)
            //{

            //var colls = store.GetCollectionsAsync().ToBlockingEnumerable().ToList();
            //
            //if (colls.Contains("Videos"))
            //{
            //    store.DeleteCollectionAsync("Videos").Wait();
            //}
            //
            //store.CreateCollectionAsync("Videos").Wait();

            // End of 'fix'

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
                .WithOpenAITextEmbeddingGenerationService(
                    modelId: options.EmbeddingModel,
                    serviceId: "textEmbedding",
                    apiKey: options.ApiKey,
                    setAsDefault: false,
                    orgId: options.Organization,
                    httpClient: null)
                .WithMemoryStorage(store)
                .Build();
                                

            return kernel;
        });
   

        services.AddTransient<ISemanticTextMemory, SemanticTextMemory>(sp =>
        {
            var store = sp.GetRequiredService<IMemoryStore>();
            var embGen = sp.GetRequiredService<ITextEmbeddingGeneration>();

            return new SemanticTextMemory(store, embGen);
        });

        services.AddTransient<IArtifactProducer, SemanticKernelArtifactProducer>();

        return services;
    }   
}