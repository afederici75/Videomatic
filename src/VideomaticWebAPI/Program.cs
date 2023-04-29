using Company.Videomatic.Application.Features.Videos.GetTranscript;
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

app.MapGet("/videos/" + nameof(GetVideosDTOQuery), 
    async ([AsParameters] GetVideosDTOQuery query,
           ISender sender) => 
    {
        var resp = await sender.Send(query);
        return Results.Ok(resp);
    });

app.MapGet("/videos/" + nameof(GetTranscriptDTOQuery),
    async ([AsParameters] GetTranscriptDTOQuery query,
           ISender sender) =>
    {
        var resp = await sender.Send(query);
        return Results.Ok(resp);
    });


app.MapPost("videos/" + nameof(ImportVideoCommand),
    async (ImportVideoCommand command,
           ISender sender) =>
    {
        var resp = await sender.Send(command);
        return Results.Ok(resp);
    });

app.MapPut("videos/" + nameof(UpdateVideoCommand),
    async (UpdateVideoCommand command,
           ISender sender) =>
    {
        var resp = await sender.Send(command);
        return Results.Ok(resp);
    });

app.MapDelete("videos/" + nameof(DeleteVideoCommand),
    async ([AsParameters] ImportVideoCommand command,
           ISender sender) =>
    {
        var resp = await sender.Send(command);
        return Results.Ok(resp);
    });

app.Run();