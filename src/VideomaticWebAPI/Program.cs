using Company.Videomatic.Application.Features;
using Company.Videomatic.Application.Features.Videos.GetTranscript;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddYouTubeInfrastructure(builder.Configuration);
builder.Services.AddSqlServerInfrastructure(builder.Configuration);
builder.Services.AddSemanticKernelInfrastructure(builder.Configuration);

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

//app.MapGet("/videos/" + nameof(GetVideosDTOQuery), 
//    async ([AsParameters] GetVideosDTOQuery query,
//           ISender sender) => 
//    {
//        var resp = await sender.Send(query);
//        return Results.Ok(resp);
//    }).res;
app.MapGet("/videos/" + nameof(GetVideosDTOQuery), GetVideosDTOQuery);

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


[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QueryResponse<VideoDTO>))]
static async Task<IResult> GetVideosDTOQuery(
    [AsParameters] GetVideosDTOQuery query,
    ISender sender)
{
    QueryResponse<VideoDTO> resp = await sender.Send(query);
    
    return TypedResults.Ok(resp);
}