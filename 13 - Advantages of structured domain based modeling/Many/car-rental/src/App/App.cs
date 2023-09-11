var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<ICarState, CarState>();
builder.Services.AddScoped<IRentRepository, RentRepository>();
builder.Services.AddScoped<IRentService, RentService>();
builder.Services.AddScoped<IRentState, RentState>();

builder.Services.AddMvc(o =>
{
    o.EnableEndpointRouting = false;
});

builder.Services.AddScoped<IExtensions, Extensions>();
builder.Services.AddScoped<IEventStream, EventStream>();
builder.Services.AddScoped<IEventHandler, Application.EventHandlers.EventHandler>();

await new DpApp(builder).Run("car-rental", (app) =>
{
    app.UseRouting();
    //Uncomment this line to enable Authentication
    app.UseAuthentication();
    DpApp.UseDevPrimeSwagger(app);
    //Uncomment this line to enable UseAuthorization
    app.UseAuthorization();
    app.MapControllerRoute(
       name: "default",
       pattern: "{controller=Home}/{action=Index}/{id?}");
}, (builder) =>
{
    DpApp.AddDevPrime(builder.Services);
    DpApp.AddDevPrimeSwagger(builder.Services);
    DpApp.AddDevPrimeSecurity(builder.Services);
});