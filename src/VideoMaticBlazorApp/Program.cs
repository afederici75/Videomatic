using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets<WebApplication>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddHttpClient();
//builder.Services.AddApplication(builder.Configuration);
//builder.Services.AddYouTubeDrivers(builder.Configuration);
//builder.Services.AddSqlServerDriver(builder.Configuration);
//builder.Services.AddSemanticKernelDriver(builder.Configuration);

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

// Register the Swagger generator and the Swagger UI middlewares
//app.UseOpenApi();
//app.UseSwaggerUi3();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
