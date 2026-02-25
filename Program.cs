
//starts your server and configures authentication

using Microsoft.AspNetCore.Authentication.Cookies; //import cookies authentication and stores login sessions.
using Microsoft.AspNetCore.Authentication.Google;//allows app to login using google acc.

var builder = WebApplication.CreateBuilder(args);//This prepares your ASP.NET backend to run

builder.Services.AddControllers();//Controllers handle API routes

builder.Services.AddAuthentication(options =>
{//Enables authentication system in your app
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;//after Google login, user session stored in cookie.
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;// redirect to google.
})
.AddCookie()// enable cookie authentication


.AddGoogle(options => //Enables Google login provider
{
    options.ClientId = "";
    options.ClientSecret = "";
});

var app = builder.Build();//builds the web app

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();//Enables controller routes

app.Run();//Starts the server


