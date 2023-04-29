using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddYouTubeDrivers(builder.Configuration);
builder.Services.AddSqlServerDriver(builder.Configuration);
builder.Services.AddSemanticKernelDriver(builder.Configuration);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/videos/" + nameof(GetVideosDTOQuery), 
    async (int? Take,
           string? TitlePrefix,
           string? DescriptionPrefix,
           string? ProviderIdPrefix,
           string? ProviderVideoIdPrefix,
           string? VideoUrlPrefix,
           int? Skip,
           string[]? Includes,
           string[]? OrderBy,
           ISender sender) => 
    {
        var qry = new GetVideosDTOQuery(Take, TitlePrefix, DescriptionPrefix, ProviderIdPrefix, ProviderVideoIdPrefix, VideoUrlPrefix, Skip, Includes, OrderBy);
        var resp = await sender.Send(qry);
        return Results.Ok(resp);
    });



app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
