namespace DevPrime.Web;
public class CustomerProfile : Routes
{
    public override void Endpoints(WebApplication app)
    {
        //Automatically returns 404 when no result  
        app.MapGet("/v1/customerprofile", async (HttpContext http, ICustomerProfileService Service, int? limit, int? offset, string ordering, string ascdesc, string filter) => await Dp(http).Pipeline(() => Service.GetAll(new Application.Services.CustomerProfile.Model.CustomerProfile(limit, offset, ordering, ascdesc, filter)), 404));
        //Automatically returns 404 when no result 
        app.MapGet("/v1/customerprofile/{id}", async (HttpContext http, ICustomerProfileService Service, Guid id) => await Dp(http).Pipeline(() => Service.Get(new Application.Services.CustomerProfile.Model.CustomerProfile(id)), 404));
        app.MapPost("/v1/customerprofile", async (HttpContext http, ICustomerProfileService Service, DevPrime.Web.Models.CustomerProfile.CustomerProfile command) => await Dp(http).Pipeline(() => Service.Add(command.ToApplication())));
        app.MapPut("/v1/customerprofile", async (HttpContext http, ICustomerProfileService Service, Application.Services.CustomerProfile.Model.CustomerProfile command) => await Dp(http).Pipeline(() => Service.Update(command)));
        app.MapDelete("/v1/customerprofile/{id}", async (HttpContext http, ICustomerProfileService Service, Guid id) => await Dp(http).Pipeline(() => Service.Delete(new Application.Services.CustomerProfile.Model.CustomerProfile(id))));
    }
}