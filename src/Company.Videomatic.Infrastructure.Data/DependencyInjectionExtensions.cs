using Company.Videomatic.Infrastructure.Data;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddVideomaticData(this IServiceCollection services, IConfiguration configuration)
    {
        // Services
        //services.AddScoped<IUnitOfWork, VideomaticDbContext>();

        return services;
    }
}