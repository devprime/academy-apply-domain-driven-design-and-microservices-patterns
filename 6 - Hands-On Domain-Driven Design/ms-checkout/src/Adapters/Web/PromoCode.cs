namespace DevPrime.Web;
public class PromoCode : Routes
{
    public override void Endpoints(WebApplication app)
    {
        //Automatically returns 404 when no result  
        app.MapGet("/v1/promocode", async (HttpContext http, IPromoCodeService Service, int? limit, int? offset, string ordering, string ascdesc, string filter) => await Dp(http).Pipeline(() => Service.GetAll(new Application.Services.PromoCode.Model.PromoCode(limit, offset, ordering, ascdesc, filter)), 404));
        //Automatically returns 404 when no result 
        app.MapGet("/v1/promocode/{id}", async (HttpContext http, IPromoCodeService Service, Guid id) => await Dp(http).Pipeline(() => Service.Get(new Application.Services.PromoCode.Model.PromoCode(id)), 404));
        app.MapPost("/v1/promocode", async (HttpContext http, IPromoCodeService Service, DevPrime.Web.Models.PromoCode.PromoCode command) => await Dp(http).Pipeline(() => Service.Add(command.ToApplication())));
        app.MapPut("/v1/promocode", async (HttpContext http, IPromoCodeService Service, Application.Services.PromoCode.Model.PromoCode command) => await Dp(http).Pipeline(() => Service.Update(command)));
        app.MapDelete("/v1/promocode/{id}", async (HttpContext http, IPromoCodeService Service, Guid id) => await Dp(http).Pipeline(() => Service.Delete(new Application.Services.PromoCode.Model.PromoCode(id))));
    }
}