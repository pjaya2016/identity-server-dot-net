using identity_mvc.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;

}).AddCookie("Cookies", options =>
{
                 
               
})
.AddOpenIdConnect("oidc", (options) =>
{
    options.Authority = "https://localhost:5443";
    options.ClientId = "interactive";
    options.ClientSecret = "SuperSecretPassword";

    options.ResponseType = "code";
    options.UsePkce = true;
    options.ResponseMode = "query";

    options.Scope.Add("weatherapi.read openid profile");
    
    options.SaveTokens = true;

});

builder.Services.AddSingleton<ITokenService, TokenService>();



var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "default",
      pattern: "{controller=Home}/{action=Index}/{id?}");
});


app.Run();
