using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAuthentication(defaultScheme: AppServicesAuthenticationDefaults.AuthenticationScheme)
    .AddAppServicesAuthentication();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();