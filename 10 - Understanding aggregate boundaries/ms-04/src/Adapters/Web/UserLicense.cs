namespace DevPrime.Web;
public class UserLicense : Routes
{
    public override void Endpoints(WebApplication app)
    {
        //Automatically returns 404 when no result  
        app.MapGet("/v1/userlicense", async (HttpContext http, IUserLicenseService Service, int? limit, int? offset, string ordering, string ascdesc, string filter) => await Dp(http).Pipeline(() => Service.GetAll(new Application.Services.UserLicense.Model.UserLicense(limit, offset, ordering, ascdesc, filter)), 404));
        //Automatically returns 404 when no result 
        app.MapGet("/v1/userlicense/{id}", async (HttpContext http, IUserLicenseService Service, Guid id) => await Dp(http).Pipeline(() => Service.Get(new Application.Services.UserLicense.Model.UserLicense(id)), 404));
        app.MapPost("/v1/userlicense", async (HttpContext http, IUserLicenseService Service, DevPrime.Web.Models.UserLicense.UserLicense command) => await Dp(http).Pipeline(() => Service.Add(command.ToApplication())));
        app.MapPut("/v1/userlicense", async (HttpContext http, IUserLicenseService Service, Application.Services.UserLicense.Model.UserLicense command) => await Dp(http).Pipeline(() => Service.Update(command)));
        app.MapDelete("/v1/userlicense/{id}", async (HttpContext http, IUserLicenseService Service, Guid id) => await Dp(http).Pipeline(() => Service.Delete(new Application.Services.UserLicense.Model.UserLicense(id))));
    }
}