namespace DevPrime.Web;
public class Account : Routes
{
    public override void Endpoints(WebApplication app)
    {
        //Automatically returns 404 when no result  
        app.MapGet("/v1/account", async (HttpContext http, IAccountService Service, int? limit, int? offset, string ordering, string ascdesc, string filter) => await Dp(http).Pipeline(() => Service.GetAll(new Application.Services.Account.Model.Account(limit, offset, ordering, ascdesc, filter)), 404));
        //Automatically returns 404 when no result 
        app.MapGet("/v1/account/{id}", async (HttpContext http, IAccountService Service, Guid id) => await Dp(http).Pipeline(() => Service.Get(new Application.Services.Account.Model.Account(id)), 404));
        
        app.MapPost("/v1/account", async (HttpContext http, IAccountService Service, DevPrime.Web.Models.Account.Account command) => await Dp(http).Pipeline(() => Service.Add(command.ToApplication())));

        app.MapPost("/v1/account/transfer", async (HttpContext http, IAccountService Service, string origin, string destination,double value) => await Dp(http).Pipeline(() => Service.BankTransfer(origin, destination, value)));
       
        app.MapPut("/v1/account", async (HttpContext http, IAccountService Service, Application.Services.Account.Model.Account command) => await Dp(http).Pipeline(() => Service.Update(command)));
        app.MapDelete("/v1/account/{id}", async (HttpContext http, IAccountService Service, Guid id) => await Dp(http).Pipeline(() => Service.Delete(new Application.Services.Account.Model.Account(id))));
    }
}