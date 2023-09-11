namespace Domain.Aggregates.Order;
public class Order : AggRoot
{
    public string CustomerTaxID { get; private set; }
    public string CustomerName { get; private set; }
    public string PromoCode { get; private set; }
    public IList<Item> Items { get; private set; }
    public double TotalPrice { get; private set; }
    public Order(Guid id, string customerTaxID, string customerName, string promoCode, IEnumerable<Domain.Aggregates.Order.Item> items, double totalPrice)
    {
        ID = id;
        CustomerTaxID = customerTaxID;
        CustomerName = customerName;
        PromoCode = promoCode;
        Items = items?.ToList();
        TotalPrice = totalPrice;
    }
    public Order()
    {
    }
    public void SetDiscount(double percentageDiscount)
    {
        Dp.Pipeline(Execute: () =>
        {
            TotalPrice = 0;
            Dp.Attach(Items);
            foreach (var item in Items)
                TotalPrice += item.SubTotal();
            TotalPrice = TotalPrice * ((100 - percentageDiscount) / 100);

        });
    }

    public virtual void Add()
    {
        Dp.Pipeline(Execute: () =>
        {
            Dp.Attach(Items);
            ValidFields();
            ID = Guid.NewGuid();
            IsNew = true;
            var success = Dp.ProcessEvent<bool>(new CreateOrder());
            if (success)
            {
                Dp.ProcessEvent(new OrderCreated());
            }
        });
    }
    public virtual void Update()
    {
        Dp.Pipeline(Execute: () =>
        {
            Dp.Attach(Items);
            if (ID.Equals(Guid.Empty))
                Dp.Notifications.Add("ID is required");
            ValidFields();
            var success = Dp.ProcessEvent<bool>(new UpdateOrder());
            if (success)
            {
                Dp.ProcessEvent(new OrderUpdated());
            }
        });
    }
    public virtual void Delete()
    {
        Dp.Pipeline(Execute: () =>
        {
            if (ID != Guid.Empty)
            {
                var success = Dp.ProcessEvent<bool>(new DeleteOrder());
                if (success)
                {
                    Dp.ProcessEvent(new OrderDeleted());
                }
            }
        });
    }
    public virtual (List<Order> Result, long Total) Get(int? limit, int? offset, string ordering, string sort, string filter)
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
                    if (filter.ToLower().StartsWith("customertaxid="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("customername="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("promocode="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("totalprice="))
                        filterIsValid = true;
                }
                if (!filterIsValid)
                    throw new PublicException($"Invalid filter '{filter}' is invalid try: 'ID', 'CustomerTaxID', 'CustomerName', 'PromoCode', 'TotalPrice',");
            }
            var source = Dp.ProcessEvent(new OrderGet()
            { Limit = limit, Offset = offset, Ordering = ordering, Sort = sort, Filter = filter });
            return source;
        });
    }
    public virtual Order GetByID()
    {
        var result = Dp.Pipeline(ExecuteResult: () =>
        {
            return Dp.ProcessEvent<Order>(new OrderGetByID());
        });
        return result;
    }
    private void ValidFields()
    {
        if (String.IsNullOrWhiteSpace(CustomerTaxID))
            Dp.Notifications.Add("CustomerTaxID is required");
        if (String.IsNullOrWhiteSpace(CustomerName))
            Dp.Notifications.Add("CustomerName is required");
        if (String.IsNullOrWhiteSpace(PromoCode))
            Dp.Notifications.Add("PromoCode is required");
        Dp.Notifications.ValidateAndThrow();
    }
}