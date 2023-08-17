using Infrastructure.Data;
using Infrastructure.Data.SqlServer;
using Hangfire;
using Radzen;
using VideomaticRadzen;
using SharedKernel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor().AddHubOptions(o =>
{
    o.MaximumReceiveMessageSize = 10 * 1024 * 1024;
});
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

builder.Services.AddVideomaticSharedKernel(builder.Configuration);
builder.Services.AddVideomaticApplication(builder.Configuration);
builder.Services.AddVideomaticData(builder.Configuration);
builder.Services.AddVideomaticDataForSqlServer(builder.Configuration);
builder.Services.AddVidematicYouTubeInfrastructure(builder.Configuration);
                 
// Add Hangfire services.
var connectionName = $"{VideomaticConstants.Videomatic}.{SqlServerVideomaticDbContext.ProviderName}";

#pragma warning disable ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'
builder.Services.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseActivator(new ContainerJobActivator(builder.Services.BuildServiceProvider()))
        .UseFilter(new AutomaticRetryAttribute { Attempts = 0 })
        .UseSqlServerStorage(builder.Configuration.GetConnectionString(connectionName),
                            new Hangfire.SqlServer.SqlServerStorageOptions()
                            {
                                PrepareSchemaIfNecessary = true,
                            }));
#pragma warning restore ASP0000 // Do not call 'IServiceCollection.BuildServiceProvider' in 'ConfigureServices'

// Add the processing server as IHostedService
builder.Services.AddHangfireServer(options => options.WorkerCount = 1); // SUPER IMPORTANT to set this to 1 for Blazor hosts! See ASP Net Core example pointed by https://github.com/sergezhigunov/Hangfire.EntityFrameworkCore

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseHangfireDashboard();


app.UseRouting();

//// Hangfire test
var hangfire = app.Services.GetService<IBackgroundJobClient>();
//var mediator = app.Services.GetService<IMediator>();
//
//var qry = new GetVideosQuery($"Test@({DateTime.Now})", null, 0, null, null, true, null, null, null);
//ISender sender = app.Services.GetRequiredService<ISender>();
////hangfire.Enqueue(() => sender.Send(qry, default));
//hangfire.Enqueue(() => Task.Delay(12000));
//hangfire.Enqueue(() => Task.Delay(12000));
//hangfire.Enqueue(() => Task.Delay(12000));
//hangfire.Enqueue(() => Task.Delay(12000));
//hangfire.Enqueue(() => Task.Delay(12000));

//app.MapGet("/a", (string? code, string? scope) => $"This is a GET -> Code:{code}, State:{scope}");
//app.MapGet("/oauthCallback", (string? code, string? scope) =>
//{
//    var str = $"Code:{code}, State:{scope}";
//
//    //RedirectResult redirect = new RedirectResult("/Videos", true);
//    return Results.RedirectToRoute("/Videos");
//});

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();