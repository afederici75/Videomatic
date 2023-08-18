using Infrastructure.YouTube;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddVidematicYouTubeInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // IOptions
        var section = configuration.GetRequiredSection("YouTube");
        services.Configure<YouTubeOptions>(section);

        // Services
        services.AddTransient<YouTubeService>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<YouTubeOptions>>().Value;
            var certificate = new X509Certificate2(@"YouTubeServiceAccount.p12", options.CertificatePassword, X509KeyStorageFlags.Exportable);

            ServiceAccountCredential credential = new(
               new ServiceAccountCredential.Initializer(options.ServiceAccountEmail)
               {
                   Scopes = new[] { YouTubeService.Scope.Youtube }
               }.FromCertificate(certificate));

            // Create the service.
            var service = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = AppDomain.CurrentDomain.FriendlyName
            });

            return service;
        });
        services.AddTransient<IVideoProvider, YouTubeVideoProvider>();
        services.AddTransient<IVideoImporter, YouTubeVideoImporter>();
        
        return services;
    }   
}