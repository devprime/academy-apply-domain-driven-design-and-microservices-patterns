﻿namespace Domain.Aggregates.Customer;
public class Customer : AggRoot
{
    public Email Email { get; private set; }
    public string Name { get; private set; }
    public Customer(Guid id, Domain.Aggregates.Customer.Email email, string name)
    {
        ID = id;
        Email = email;
        Name = name;
    }
    public Customer()
    {
    }
    public virtual void Add()
    {
        Dp.Pipeline(Execute: () =>
        {
            Dp.Attach(Email);
            ValidFields();
            ID = Guid.NewGuid();
            IsNew = true;
            var success = Dp.ProcessEvent<bool>(new CreateCustomer());
        });
    }
    public virtual void Update()
    {
        Dp.Pipeline(Execute: () =>
        {
            Dp.Attach(Email);
            if (ID.Equals(Guid.Empty))
                Dp.Notifications.Add("ID is required");
            ValidFields();
            var success = Dp.ProcessEvent<bool>(new UpdateCustomer());
        });
    }
    public virtual void Delete()
    {
        Dp.Pipeline(Execute: () =>
        {
            if (ID != Guid.Empty)
            {
                var success = Dp.ProcessEvent<bool>(new DeleteCustomer());
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
                }
                if (!filterIsValid)
                    throw new PublicException($"Invalid filter '{filter}' is invalid try: 'ID', 'Name',");
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
         if (!Email.Validate())
            Dp.Notifications.Add($"Email `{Email.Value}` is invalid");

        Dp.Notifications.ValidateAndThrow();
   
    }
}