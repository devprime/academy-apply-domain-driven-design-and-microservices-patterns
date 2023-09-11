namespace DevPrime.Web;
public class Car : Routes
{
    public override void Endpoints(WebApplication app)
    {
        //Automatically returns 404 when no result  
        app.MapGet("/v1/car", async (HttpContext http, ICarService Service, int? limit, int? offset, string ordering, string ascdesc, string filter) => await Dp(http).Pipeline(() => Service.GetAll(new Application.Services.Car.Model.Car(limit, offset, ordering, ascdesc, filter)), 404));
        //Automatically returns 404 when no result 
        app.MapGet("/v1/car/{id}", async (HttpContext http, ICarService Service, Guid id) => await Dp(http).Pipeline(() => Service.Get(new Application.Services.Car.Model.Car(id)), 404));
        app.MapPost("/v1/car", async (HttpContext http, ICarService Service, DevPrime.Web.Models.Car.Car command) => await Dp(http).Pipeline(() => Service.Add(command.ToApplication())));
        app.MapPut("/v1/car", async (HttpContext http, ICarService Service, Application.Services.Car.Model.Car command) => await Dp(http).Pipeline(() => Service.Update(command)));
        app.MapDelete("/v1/car/{id}", async (HttpContext http, ICarService Service, Guid id) => await Dp(http).Pipeline(() => Service.Delete(new Application.Services.Car.Model.Car(id))));
    }
}