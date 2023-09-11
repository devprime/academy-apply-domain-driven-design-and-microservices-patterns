﻿namespace Domain.Aggregates.Order;
public class Order : AggRoot
{
    public string CustomerName { get; private set; }
    public string CustomerEmail { get; private set; }
    public IList<Item> Items { get; private set; }
    public Order(Guid id, string customerName, string customerEmail, IEnumerable<Domain.Aggregates.Order.Item> items)
    {
        ID = id;
        CustomerName = customerName;
        CustomerEmail = customerEmail;
        Items = items?.ToList();
    }
    public Order()
    {
    }
    public virtual void Add()
    {
        Dp.Pipeline(Execute: () =>
        {
            Dp.Attach(Items);
            ValidFields();
            ID = Guid.NewGuid();
            var customerexists = Dp.ProcessEvent<bool>(new CustomerExists(CustomerEmail));
            if(!customerexists)
            {
               Dp.ProcessEvent(new CustomerIsNew(CustomerEmail, CustomerName));
            }

            IsNew = true;
            Dp.ProcessEvent(new CreateOrder());
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
        });
    }
    public virtual void Delete()
    {
        Dp.Pipeline(Execute: () =>
        {
            if (ID != Guid.Empty)
            {
                var success = Dp.ProcessEvent<bool>(new DeleteOrder());
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
                    if (filter.ToLower().StartsWith("customername="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("customeremail="))
                        filterIsValid = true;
                }
                if (!filterIsValid)
                    throw new PublicException($"Invalid filter '{filter}' is invalid try: 'ID', 'CustomerName', 'CustomerEmail',");
            }
            var source = Dp.ProcessEvent(new OrderGet()
            {Limit = limit, Offset = offset, Ordering = ordering, Sort = sort, Filter = filter});
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
        if (String.IsNullOrWhiteSpace(CustomerName))
            Dp.Notifications.Add("CustomerName is required");
        if (String.IsNullOrWhiteSpace(CustomerEmail))
            Dp.Notifications.Add("CustomerEmail is required");
        Dp.Notifications.ValidateAndThrow();
    }
}