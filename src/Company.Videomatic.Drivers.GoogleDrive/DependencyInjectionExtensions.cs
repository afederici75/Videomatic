using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Drivers.GoogleDrive;
using Company.Videomatic.Drivers.GoogleDrive.Options;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddGoogleDriveDrivers(this IServiceCollection services, IConfiguration configuration)
    {
        // IOptions
        var section = configuration.GetRequiredSection("GoogleDrive");
        services.Configure<GoogleDriveOptions>(section);

        // Services
        //services.AddScoped<IVideoProvider, MockGoogleDriveVideoProvider>();
        return services;
    }   
}