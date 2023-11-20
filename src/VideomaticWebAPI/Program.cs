using Infrastructure.Data;
using Infrastructure.Data.Seeder;
using Microsoft.EntityFrameworkCore;
using VideomaticWebAPI;

var builder = WebApplication.CreateBuilder(args);

// Videomatic
builder.Configuration.SetupVideomaticConfiguration();
builder.Services.AddVideomaticServer(builder.Configuration, false);

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
//    [AsParameters] CommandContract query,
//    ISender sender)
//{
//    QueryResponse<GetVideosResult> resp = await sender.Send(query);
//    
//    return TypedResults.Ok(resp);
//}