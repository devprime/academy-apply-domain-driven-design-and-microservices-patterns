namespace DevPrime.Web;
public class License : Routes
{
    public override void Endpoints(WebApplication app)
    {
        //Automatically returns 404 when no result  
        app.MapGet("/v1/license", async (HttpContext http, ILicenseService Service, int? limit, int? offset, string ordering, string ascdesc, string filter) => await Dp(http).Pipeline(() => Service.GetAll(new Application.Services.License.Model.License(limit, offset, ordering, ascdesc, filter)), 404));
        //Automatically returns 404 when no result 
        app.MapGet("/v1/license/{id}", async (HttpContext http, ILicenseService Service, Guid id) => await Dp(http).Pipeline(() => Service.Get(new Application.Services.License.Model.License(id)), 404));
        app.MapPost("/v1/license", async (HttpContext http, ILicenseService Service, DevPrime.Web.Models.License.License command) => await Dp(http).Pipeline(() => Service.Add(command.ToApplication())));
        app.MapPut("/v1/license", async (HttpContext http, ILicenseService Service, Application.Services.License.Model.License command) => await Dp(http).Pipeline(() => Service.Update(command)));
        app.MapDelete("/v1/license/{id}", async (HttpContext http, ILicenseService Service, Guid id) => await Dp(http).Pipeline(() => Service.Delete(new Application.Services.License.Model.License(id))));
    }
}