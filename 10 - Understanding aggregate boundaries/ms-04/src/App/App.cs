var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ILicenseRepository, LicenseRepository>();
builder.Services.AddScoped<ILicenseService, LicenseService>();
builder.Services.AddScoped<ILicenseState, LicenseState>();
builder.Services.AddScoped<IUserLicenseRepository, UserLicenseRepository>();
builder.Services.AddScoped<IUserLicenseService, UserLicenseService>();
builder.Services.AddScoped<IUserLicenseState, UserLicenseState>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserState, UserState>();
builder.Services.AddMvc(o =>
{
    o.EnableEndpointRouting = false;
});
builder.Services.AddScoped<IExtensions, Extensions>();
builder.Services.AddScoped<IEventStream, EventStream>();
builder.Services.AddScoped<IEventHandler, Application.EventHandlers.EventHandler>();
await new DpApp(builder).Run("ms-04", (app) =>
{
    app.UseRouting();
    //Uncomment this line to enable Authentication
    app.UseAuthentication();
    DpApp.UseDevPrimeSwagger(app);
    //Uncomment this line to enable UseAuthorization
    app.UseAuthorization();
    app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
}, (builder) =>
{
    DpApp.AddDevPrime(builder.Services);
    DpApp.AddDevPrimeSwagger(builder.Services);
    DpApp.AddDevPrimeSecurity(builder.Services);
});