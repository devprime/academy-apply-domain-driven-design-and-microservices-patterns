namespace DevPrime.Web.Models.PromoCode;
public class PromoCode
{
    public string Code { get; set; }
    public double PercentageDiscount { get; set; }
    public bool Active { get; set; }
    public DateTime ValidUntil { get; set; }
    public static Application.Services.PromoCode.Model.PromoCode ToApplication(DevPrime.Web.Models.PromoCode.PromoCode promoCode)
    {
        if (promoCode is null)
            return new Application.Services.PromoCode.Model.PromoCode();
        Application.Services.PromoCode.Model.PromoCode _promoCode = new Application.Services.PromoCode.Model.PromoCode();
        _promoCode.Code = promoCode.Code;
        _promoCode.PercentageDiscount = promoCode.PercentageDiscount;
        _promoCode.Active = promoCode.Active;
        _promoCode.ValidUntil = promoCode.ValidUntil;
        return _promoCode;
    }
    public static List<Application.Services.PromoCode.Model.PromoCode> ToApplication(IList<DevPrime.Web.Models.PromoCode.PromoCode> promoCodeList)
    {
        List<Application.Services.PromoCode.Model.PromoCode> _promoCodeList = new List<Application.Services.PromoCode.Model.PromoCode>();
        if (promoCodeList != null)
        {
            foreach (var promoCode in promoCodeList)
            {
                Application.Services.PromoCode.Model.PromoCode _promoCode = new Application.Services.PromoCode.Model.PromoCode();
                _promoCode.Code = promoCode.Code;
                _promoCode.PercentageDiscount = promoCode.PercentageDiscount;
                _promoCode.Active = promoCode.Active;
                _promoCode.ValidUntil = promoCode.ValidUntil;
                _promoCodeList.Add(_promoCode);
            }
        }
        return _promoCodeList;
    }
    public virtual Application.Services.PromoCode.Model.PromoCode ToApplication()
    {
        var model = ToApplication(this);
        return model;
    }
}