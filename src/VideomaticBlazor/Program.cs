using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Company.Videomatic.Infrastructure.Data;
using Company.Videomatic.Infrastructure.Data.Seeder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddVideomaticApplication(builder.Configuration);
builder.Services.AddVideomaticData(builder.Configuration);
builder.Services.AddVideomaticDataForSqlServer(builder.Configuration);

#region Blazorise

builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrap5Providers()
    .AddFontAwesomeIcons();

#endregion

//builder.Services.AddCors(options =>
//    {
//        options.AddPolicy("NewPolicy", builder =>
//         builder.WithOrigins()
//                      .AllowAnyMethod()
//                      .AllowAnyHeader()
//                      .AllowCredentials());
//    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


//var dbContext = app.Services.GetRequiredService<>();
//await dbContext.Database.EnsureCreatedAsync();


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

//app.UseCors("NewPolicy");


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<VideomaticDbContext>();
    db.Database.Migrate();


    //var seeder = app.Services.GetService<IDbSeeder>();
    //await seeder!.SeedAsync();
}

app.Run();
