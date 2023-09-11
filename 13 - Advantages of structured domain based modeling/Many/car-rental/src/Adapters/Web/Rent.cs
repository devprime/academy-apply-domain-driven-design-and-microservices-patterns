namespace DevPrime.Web;
public class Rent : Routes
{
    public override void Endpoints(WebApplication app)
    {
        //Automatically returns 404 when no result  
        app.MapGet("/v1/rent", async (HttpContext http, IRentService Service, int? limit, int? offset, string ordering, string ascdesc, string filter) => await Dp(http).Pipeline(() => Service.GetAll(new Application.Services.Rent.Model.Rent(limit, offset, ordering, ascdesc, filter)), 404));
        //Automatically returns 404 when no result 
        app.MapGet("/v1/rent/{id}", async (HttpContext http, IRentService Service, Guid id) => await Dp(http).Pipeline(() => Service.Get(new Application.Services.Rent.Model.Rent(id)), 404));
        app.MapPost("/v1/rent", async (HttpContext http, IRentService Service, DevPrime.Web.Models.Rent.Rent command) => await Dp(http).Pipeline(() => Service.Add(command.ToApplication())));
        app.MapPut("/v1/rent", async (HttpContext http, IRentService Service, Application.Services.Rent.Model.Rent command) => await Dp(http).Pipeline(() => Service.Update(command)));
        app.MapDelete("/v1/rent/{id}", async (HttpContext http, IRentService Service, Guid id) => await Dp(http).Pipeline(() => Service.Delete(new Application.Services.Rent.Model.Rent(id))));
    }
}