namespace Domain.Aggregates.Customer;
public class Customer : AggRoot
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public Customer(Guid id, string name, string email)
    {
        ID = id;
        Name = name;
        Email = email;
    }
    public Customer()
    {
    }
    public virtual void Add()
    {
        Dp.Pipeline(Execute: () =>
        {
            ValidFields();
            ID = Guid.NewGuid();
            IsNew = true;
            var success = Dp.ProcessEvent<bool>(new CreateCustomer());
            if (success)
            {
                Dp.ProcessEvent(new CustomerCreated());
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
            var success = Dp.ProcessEvent<bool>(new UpdateCustomer());
            if (success)
            {
                Dp.ProcessEvent(new CustomerUpdated());
            }
        });
    }
    public virtual void Delete()
    {
        Dp.Pipeline(Execute: () =>
        {
            if (ID != Guid.Empty)
            {
                var success = Dp.ProcessEvent<bool>(new DeleteCustomer());
                if (success)
                {
                    Dp.ProcessEvent(new CustomerDeleted());
                }
            }
        });
    }
    public virtual (List<Customer> Result, long Total) Get(int? limit, int? offset, string ordering, string sort, string filter)
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
                    if (filter.ToLower().StartsWith("name="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("email="))
                        filterIsValid = true;
                }
                if (!filterIsValid)
                    throw new PublicException($"Invalid filter '{filter}' is invalid try: 'ID', 'Name', 'Email',");
            }
            var source = Dp.ProcessEvent(new CustomerGet()
            {Limit = limit, Offset = offset, Ordering = ordering, Sort = sort, Filter = filter});
            return source;
        });
    }
    public virtual Customer GetByID()
    {
        var result = Dp.Pipeline(ExecuteResult: () =>
        {
            return Dp.ProcessEvent<Customer>(new CustomerGetByID());
        });
        return result;
    }
    private void ValidFields()
    {
        if (String.IsNullOrWhiteSpace(Name))
            Dp.Notifications.Add("Name is required");
        if (String.IsNullOrWhiteSpace(Email))
            Dp.Notifications.Add("Email is required");
        Dp.Notifications.ValidateAndThrow();
    }
}