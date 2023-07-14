﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Integration.Tests;

public class Startup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        var cfg = LoadConfiguration();

        services.AddLogging(x => x.AddConsole());

        services.AddVideomaticApplication(cfg);
        services.AddVidematicYouTubeInfrastructure(cfg);
        services.AddVideomaticSemanticKernel(cfg);
        services.AddVideomaticData(cfg);
        services.AddVideomaticDataForSqlServer(cfg);

        //services.AddScoped<IVideoImporter, MockVideoImporter>();
        //services.AddScoped<IVideoAnalyzer, MockVideoAnalyzer>();
    }

    public static IConfiguration LoadConfiguration()
    {
        return new ConfigurationBuilder()
                        .AddJsonFile("testSettings.json", false)
                        .AddUserSecrets(typeof(Startup).Assembly)
                        .Build();
    }
}