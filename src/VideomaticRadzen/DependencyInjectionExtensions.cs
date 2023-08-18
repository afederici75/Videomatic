using Hangfire;
using Infrastructure.Data.SqlServer;
using Infrastructure.Data;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjectionExtensions
{
    public static void SetupVideomaticConfiguration(this IConfigurationBuilder builder)
    {
        builder.SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("logsettings.json", false, true)
               .AddJsonFile("logsettings.Development.json", true, true)
               .AddJsonFile("appsettings.json", false, true)
               .AddJsonFile("appsettings.Development.json", true, true)
               .AddUserSecrets<Program>(false, true)
               .AddEnvironmentVariables();
    }

    public static void AddVideomaticServer(
        this IServiceCollection services,
        IConfiguration configuration,
        bool addHangfireHostedService,
        int? workerCount = null,
        bool registerDbContextFactory = true)
    {
        // Videomatic
        services.AddVideomaticSharedKernel(configuration);
        services.AddVideomaticApplication(configuration);
        services.AddVideomaticData(configuration);
        services.AddVideomaticDataForSqlServer(configuration, registerDbContextFactory: registerDbContextFactory);
        services.AddVidematicYouTubeInfrastructure(configuration);

        var connectionName = $"{VideomaticConstants.Videomatic}.{SqlServerVideomaticDbContext.ProviderName}";

#pragma warning disable ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'
        services.AddHangfire(cfg => cfg
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseActivator(new ContainerJobActivator(services.BuildServiceProvider()))
                .UseFilter(new AutomaticRetryAttribute { 
                    DelaysInSeconds = new[] { 1, 2, 3, 4, 5 },
                    Attempts = 5 })
                .UseSqlServerStorage(configuration.GetConnectionString(connectionName),
                                    new Hangfire.SqlServer.SqlServerStorageOptions()
                                    {
                                        PrepareSchemaIfNecessary = true,
                                    }));
#pragma warning restore ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'

        // Add the processing server as IHostedService
        if (addHangfireHostedService)
        {
            // SUPER IMPORTANT to set this to 1 for Blazor hosts!
            // See ASP Net Core example pointed by https://github.com/sergezhigunov/Hangfire.EntityFrameworkCore
            services.AddHangfireServer(options =>
            {                
                if (workerCount != null)
                    options.WorkerCount = workerCount.Value;
            }); 
        }
    }

    public class ContainerJobActivator : JobActivator
    {
        readonly IServiceProvider Provider;

        public ContainerJobActivator(IServiceProvider provider)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public override object ActivateJob(Type type)
        {
            return Provider.GetRequiredService(type);
        }
    }
}
