using Application.Features.Videos.Commands;
using Infrastructure.Data;
using Infrastructure.Data.Seeder;
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
    case Infrastructure.Data.SqlServer.SqlServerVideomaticDbContext.ProviderName:
        builder.Services.AddVideomaticDataForSqlServer(builder.Configuration);
        break;
    //case Infrastructure.Data.Sqlite.SqliteVideomaticDbContext.ProviderName:
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
        
        IDbSeeder seeder = scope.ServiceProvider.GetRequiredService<IDbSeeder>();
        
        await seeder.SeedAsync();        
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