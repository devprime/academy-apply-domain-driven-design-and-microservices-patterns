namespace Application.Services.PromoCode.Model;
public class PromoCode
{
    internal int? Limit { get; set; }
    internal int? Offset { get; set; }
    internal string Ordering { get; set; }
    internal string Filter { get; set; }
    internal string Sort { get; set; }
    public PromoCode(int? limit, int? offset, string ordering, string sort, string filter)
    {
        Limit = limit;
        Offset = offset;
        Ordering = ordering;
        Filter = filter;
        Sort = sort;
    }
    public Guid ID { get; set; }
    public string Code { get; set; }
    public double PercentageDiscount { get; set; }
    public bool Active { get; set; }
    public DateTime ValidUntil { get; set; }
    public virtual PagingResult<IList<PromoCode>> ToPromoCodeList(IList<Domain.Aggregates.PromoCode.PromoCode> promoCodeList, long? total, int? offSet, int? limit)
    {
        var _promoCodeList = ToApplication(promoCodeList);
        return new PagingResult<IList<PromoCode>>(_promoCodeList, total, offSet, limit);
    }
    public virtual PromoCode ToPromoCode(Domain.Aggregates.PromoCode.PromoCode promoCode)
    {
        var _promoCode = ToApplication(promoCode);
        return _promoCode;
    }
    public virtual Domain.Aggregates.PromoCode.PromoCode ToDomain()
    {
        var _promoCode = ToDomain(this);
        return _promoCode;
    }
    public virtual Domain.Aggregates.PromoCode.PromoCode ToDomain(Guid id)
    {
        var _promoCode = new Domain.Aggregates.PromoCode.PromoCode();
        _promoCode.ID = id;
        return _promoCode;
    }
    public PromoCode()
    {
    }
    public PromoCode(Guid id)
    {
        ID = id;
    }
    public static Application.Services.PromoCode.Model.PromoCode ToApplication(Domain.Aggregates.PromoCode.PromoCode promoCode)
    {
        if (promoCode is null)
            return new Application.Services.PromoCode.Model.PromoCode();
        Application.Services.PromoCode.Model.PromoCode _promoCode = new Application.Services.PromoCode.Model.PromoCode();
        _promoCode.ID = promoCode.ID;
        _promoCode.Code = promoCode.Code;
        _promoCode.PercentageDiscount = promoCode.PercentageDiscount;
        _promoCode.Active = promoCode.Active;
        _promoCode.ValidUntil = promoCode.ValidUntil;
        return _promoCode;
    }
    public static List<Application.Services.PromoCode.Model.PromoCode> ToApplication(IList<Domain.Aggregates.PromoCode.PromoCode> promoCodeList)
    {
        List<Application.Services.PromoCode.Model.PromoCode> _promoCodeList = new List<Application.Services.PromoCode.Model.PromoCode>();
        if (promoCodeList != null)
        {
            foreach (var promoCode in promoCodeList)
            {
                Application.Services.PromoCode.Model.PromoCode _promoCode = new Application.Services.PromoCode.Model.PromoCode();
                _promoCode.ID = promoCode.ID;
                _promoCode.Code = promoCode.Code;
                _promoCode.PercentageDiscount = promoCode.PercentageDiscount;
                _promoCode.Active = promoCode.Active;
                _promoCode.ValidUntil = promoCode.ValidUntil;
                _promoCodeList.Add(_promoCode);
            }
        }
        return _promoCodeList;
    }
    public static Domain.Aggregates.PromoCode.PromoCode ToDomain(Application.Services.PromoCode.Model.PromoCode promoCode)
    {
        if (promoCode is null)
            return new Domain.Aggregates.PromoCode.PromoCode();
        Domain.Aggregates.PromoCode.PromoCode _promoCode = new Domain.Aggregates.PromoCode.PromoCode(promoCode.ID, promoCode.Code, promoCode.PercentageDiscount, promoCode.Active, promoCode.ValidUntil);
        return _promoCode;
    }
    public static List<Domain.Aggregates.PromoCode.PromoCode> ToDomain(IList<Application.Services.PromoCode.Model.PromoCode> promoCodeList)
    {
        List<Domain.Aggregates.PromoCode.PromoCode> _promoCodeList = new List<Domain.Aggregates.PromoCode.PromoCode>();
        if (promoCodeList != null)
        {
            foreach (var promoCode in promoCodeList)
            {
                Domain.Aggregates.PromoCode.PromoCode _promoCode = new Domain.Aggregates.PromoCode.PromoCode(promoCode.ID, promoCode.Code, promoCode.PercentageDiscount, promoCode.Active, promoCode.ValidUntil);
                _promoCodeList.Add(_promoCode);
            }
        }
        return _promoCodeList;
    }
}