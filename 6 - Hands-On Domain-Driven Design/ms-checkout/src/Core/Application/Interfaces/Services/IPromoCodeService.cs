namespace Application.Interfaces.Services;
public interface IPromoCodeService
{
    void Add(Application.Services.PromoCode.Model.PromoCode command);
    void Update(Application.Services.PromoCode.Model.PromoCode command);
    void Delete(Application.Services.PromoCode.Model.PromoCode command);
    Application.Services.PromoCode.Model.PromoCode Get(Application.Services.PromoCode.Model.PromoCode query);
    PagingResult<IList<Application.Services.PromoCode.Model.PromoCode>> GetAll(Application.Services.PromoCode.Model.PromoCode query);
}