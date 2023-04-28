using Company.Videomatic.Infrastructure.SqlServer;

using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddSqlServerDriver(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<VideomaticDbContext>(builder =>
        {
            var connStr = configuration.GetConnectionString("Videomatic");
        
            builder.EnableSensitiveDataLogging()
                   .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                   .UseSqlServer(connStr);
        });

        //services.AddDbContextFactory<VideomaticDbContext>(options =>
        //{
        //    var connStr = configuration.GetConnectionString("Videomatic");
        //    
        //    options.EnableSensitiveDataLogging()
        //           .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
        //           .UseSqlServer(connStr);
        //});

       // Services
       services.AddScoped(typeof(IRepository<>), typeof(VideomaticRepository<>)); // Ardalis.Specification 

        return services;
    }   
}