
using Infrastructure.AzureSpeech;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddAzureSpeech(this IServiceCollection services, IConfiguration configuration)
    {
        // IOptions
        var cfg1 = configuration.GetSection("AzureSpeech");
        var va = cfg1["ApiKey"];

        services.Configure<AzureSpeechOptions>(configuration.GetSection("AzureSpeech"));


        // Services
        services.AddScoped<ITranscriber, Transcriber>();
        return services;
    }   
}