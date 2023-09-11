namespace Domain.Aggregates.PromoCode;
public class PromoCode : AggRoot
{
    public string Code { get; private set; }
    public double PercentageDiscount { get; private set; }
    public bool Active { get; private set; }
    public DateTime ValidUntil { get; private set; }
    public PromoCode(Guid id, string code, double percentageDiscount, bool active, DateTime validUntil)
    {
        ID = id;
        Code = code;
        PercentageDiscount = percentageDiscount;
        Active = active;
        ValidUntil = validUntil;
    }
    public PromoCode()
    {
    }
    public void Disable()
    {
        Dp.Pipeline(Execute: () =>
        {
            Active = false;
        });
    }

    public virtual void Add()
    {
        Dp.Pipeline(Execute: () =>
        {
            ValidFields();
            ID = Guid.NewGuid();
            IsNew = true;
            var success = Dp.ProcessEvent<bool>(new CreatePromoCode());
            if (success)
            {
                Dp.ProcessEvent(new PromoCodeCreated());
            }
        });
    }
    public virtual void Update()
    {
        Dp.Pipeline(Execute: () =>
        {
            if (ID.Equals(Guid.Empty))
                Dp.Notifications.Add("ID is required");
            ValidFields();
            var success = Dp.ProcessEvent<bool>(new UpdatePromoCode());
            if (success)
            {
                Dp.ProcessEvent(new PromoCodeUpdated());
            }
        });
    }
    public virtual void Delete()
    {
        Dp.Pipeline(Execute: () =>
        {
            if (ID != Guid.Empty)
            {
                var success = Dp.ProcessEvent<bool>(new DeletePromoCode());
                if (success)
                {
                    Dp.ProcessEvent(new PromoCodeDeleted());
                }
            }
        });
    }
    public virtual (List<PromoCode> Result, long Total) Get(int? limit, int? offset, string ordering, string sort, string filter)
    {
        return Dp.Pipeline(ExecuteResult: () =>
        {
            ValidateOrdering(limit, offset, ordering, sort);
            if (!string.IsNullOrWhiteSpace(filter))
            {
                bool filterIsValid = false;
                if (filter.Contains("="))
                {
                    if (filter.ToLower().StartsWith("id="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("code="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("percentagediscount="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("active="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("validuntil="))
                        filterIsValid = true;
                }
                if (!filterIsValid)
                    throw new PublicException($"Invalid filter '{filter}' is invalid try: 'ID', 'Code', 'PercentageDiscount', 'Active', 'ValidUntil',");
            }
            var source = Dp.ProcessEvent(new PromoCodeGet()
            { Limit = limit, Offset = offset, Ordering = ordering, Sort = sort, Filter = filter });
            return source;
        });
    }
    public virtual PromoCode GetByID()
    {
        var result = Dp.Pipeline(ExecuteResult: () =>
        {
            return Dp.ProcessEvent<PromoCode>(new PromoCodeGetByID());
        });
        return result;
    }
    private void ValidFields()
    {
        if (String.IsNullOrWhiteSpace(Code))
            Dp.Notifications.Add("Code is required");
        if (ValidUntil == DateTime.MinValue)
            Dp.Notifications.Add("ValidUntil is required");
        Dp.Notifications.ValidateAndThrow();
    }
}