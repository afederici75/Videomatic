using Company.Videomatic.Application.Abstractions;
using Company.Videomatic.Infrastructure.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddSqlServerDriver(this IServiceCollection services, IConfiguration configuration)
    {
        // IOptions
        services.AddDbContext<VideomaticDbContext>(builder =>
        {
            var connStr = configuration.GetConnectionString("Videomatic");

            builder.EnableSensitiveDataLogging()
                   .UseSqlServer(connStr);
        });

        // Services
        services.AddScoped<IVideoStorage, VideomaticDbContext>();

        return services;
    }   
}