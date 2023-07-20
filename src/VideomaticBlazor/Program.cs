using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddVideomaticApplication(builder.Configuration);
builder.Services.AddVideomaticData(builder.Configuration);
builder.Services.AddVideomaticDataForSqlServer(builder.Configuration);

//builder.Services.AddMediatR(cfg =>
//{
//    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
//});

#region Blazorise

builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    //.AddBootstrapProviders()
    .AddBootstrap5Providers()
    //.AddBulmaProviders()
    //.AddAntDesignProviders()
    //.AddTailwindProviders()
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

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

//app.UseCors("NewPolicy");

app.Run();
