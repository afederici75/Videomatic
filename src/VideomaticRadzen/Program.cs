using Radzen;

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

//builder.Services.AddAuthentication(o =>
//{
//    // This forces challenge results to be handled by Google OpenID Handler, so there's no
//    // need to add an AccountController that emits challenges for Login.
//    o.DefaultChallengeScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
//    // This forces forbid results to be handled by Google OpenID Handler, which checks if
//    // extra scopes are required and does automatic incremental auth.
//    o.DefaultForbidScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
//    // Default scheme that will handle everything else.
//    // Once a user is authenticated, the OAuth2 token info is stored in cookies.
//    o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//})
//        .AddCookie()
//        .AddGoogleOpenIdConnect(options =>
//        {
//            options.ClientId = builder.Configuration["YouTube:ClientId"];
//            options.ClientSecret = builder.Configuration["YouTube:ClientSecret"];
//        });

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

//app.UseAuthentication();
//app.UseAuthorization();

app.MapGet("/a", (string? code, string? scope) => $"This is a GET -> Code:{code}, State:{scope}");
app.MapGet("/oauthCallback", (string? code, string? scope) =>
{
    var str = $"Code:{code}, State:{scope}";

    //RedirectResult redirect = new RedirectResult("/Videos", true);
    return Results.RedirectToRoute("/Videos");
});


app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();