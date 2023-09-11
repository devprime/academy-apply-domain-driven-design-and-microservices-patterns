using Domain.DomainServices;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountState, AccountState>();
//Uncoment this lines depends on which database do you want to use
builder.Services.AddDbContext<ConnectionEF>(options => options.UseSqlServer(StateAdapter.GetConnection("State1").ConnectionString).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
//builder.Services.AddDbContext<ConnectionEF>(options => options.UseNpgsql(StateAdapter.GetConnection("State1").ConnectionString).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
//builder.Services.AddDbContext<ConnectionEF>(options => options.UseMySql(StateAdapter.GetConnection("State1").ConnectionString, ServerVersion.Parse(StateAdapter.GetConnection("State1").DataBaseVersion)).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

builder.Services.AddMvc(o =>
{
    o.EnableEndpointRouting = false;
});
builder.Services.AddScoped<ITransfer, Transfer>();
builder.Services.AddScoped<IExtensions, Extensions>();
builder.Services.AddScoped<IEventStream, EventStream>();
builder.Services.AddScoped<IEventHandler, Application.EventHandlers.EventHandler>();
await new DpApp(builder).Run("bank", (app) =>
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