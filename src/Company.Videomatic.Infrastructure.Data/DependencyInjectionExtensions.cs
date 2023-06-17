namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddVideomaticData(this IServiceCollection services, IConfiguration configuration)
    {
        // Services
        services.AddScoped(typeof(IRepository<>), typeof(VideomaticRepository<>)); 
        //services.AddScoped(typeof(IReadOnlyRepository<>), typeof(VideomaticRepository<>)); // Ardalis.Specification 

        return services;
    }
}