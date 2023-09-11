namespace DevPrime.Web;
public class User : Routes
{
    public override void Endpoints(WebApplication app)
    {
        //Automatically returns 404 when no result  
        app.MapGet("/v1/user", async (HttpContext http, IUserService Service, int? limit, int? offset, string ordering, string ascdesc, string filter) => await Dp(http).Pipeline(() => Service.GetAll(new Application.Services.User.Model.User(limit, offset, ordering, ascdesc, filter)), 404));
        //Automatically returns 404 when no result 
        app.MapGet("/v1/user/{id}", async (HttpContext http, IUserService Service, Guid id) => await Dp(http).Pipeline(() => Service.Get(new Application.Services.User.Model.User(id)), 404));
        app.MapPost("/v1/user", async (HttpContext http, IUserService Service, DevPrime.Web.Models.User.User command) => await Dp(http).Pipeline(() => Service.Add(command.ToApplication())));
        app.MapPut("/v1/user", async (HttpContext http, IUserService Service, Application.Services.User.Model.User command) => await Dp(http).Pipeline(() => Service.Update(command)));
        app.MapDelete("/v1/user/{id}", async (HttpContext http, IUserService Service, Guid id) => await Dp(http).Pipeline(() => Service.Delete(new Application.Services.User.Model.User(id))));
    }
}