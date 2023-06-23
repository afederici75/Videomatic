using Company.Videomatic.Application.Features.Videos.Commands;
using Company.Videomatic.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using VideomaticWebAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddVideomaticApplication(builder.Configuration);
builder.Services.AddVideomaticSemanticKernel(builder.Configuration);
builder.Services.AddVidematicYouTubeInfrastructure(builder.Configuration);
builder.Services.AddVideomaticData(builder.Configuration);

var provider = builder.Configuration.GetValue<string>("Provider");
//provider = "Sqlite";
switch (provider)
{
    case Company.Videomatic.Infrastructure.Data.SqlServer.SqlServerVideomaticDbContext.ProviderName:
        builder.Services.AddVideomaticDataForSqlServer(builder.Configuration);
        break;
    //case Company.Videomatic.Infrastructure.Data.Sqlite.SqliteVideomaticDbContext.ProviderName:
    //    builder.Services.AddVideomaticDataForSqlite(builder.Configuration);
    //    break;
    default:
        throw new ArgumentException($"Unsupported provider '{provider}'.");
}

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<VideomaticDbContext>();
        db.Database.Migrate();
        //if (VideoDataGenerator.HasData() && !db.Videos.Any())
        //{
        //    //var logger = app.Services.GetRequiredService<ILogger<VideomaticDbContext>>();
        //    //logger.LogWarning("Inserting test data...");
        //    //// Adds test data if the db is new
        //    //Video[] allVideos = VideoDataGenerator.CreateAllVideos(true).Result;
        //    //db.Videos.AddRange(allVideos);
        //    //db.SaveChanges();
        //}
    }

    app.UseSwagger();
    app.UseSwaggerUI();    
}

app.UseHttpsRedirection();
app.MapVideomaticEndpoints();

app.Run();


//[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QueryResponse<GetVideosResult>))]
//static async Task<IResult> GetVideosDTOQuery(
//    [AsParameters] GetVideosByIdQuery query,
//    ISender sender)
//{
//    QueryResponse<GetVideosResult> resp = await sender.Send(query);
//    
//    return TypedResults.Ok(resp);
//}