using Company.Videomatic.Infrastructure.Data;
using Company.Videomatic.Infrastructure.Data.SqlServer;
using Hangfire;
using Radzen;
using Serilog;

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

builder.Services.AddVideomaticApplication(builder.Configuration);
builder.Services.AddVideomaticData(builder.Configuration);
builder.Services.AddVideomaticDataForSqlServer(builder.Configuration);
builder.Services.AddVidematicYouTubeInfrastructure(builder.Configuration);

// Add Hangfire services.
var connectionName = $"{VideomaticConstants.Videomatic}.{SqlServerVideomaticDbContext.ProviderName}";

builder.Services.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(builder.Configuration.GetConnectionString(connectionName)));

// Add the processing server as IHostedService
builder.Services.AddHangfireServer();

// Use Serilog
builder.Logging.ClearProviders();

var cfg = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration);
Serilog.Core.Logger logger = cfg.CreateLogger();
builder.Services.AddLogging(bld =>
{
    bld.AddSerilog(logger: logger, dispose: true);
});

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

// Hangfire test
var hangfire = app.Services.GetService<IBackgroundJobClient>();
hangfire.Enqueue(() => Console.WriteLine("Videomatic connected to Hangfire successfully."));

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