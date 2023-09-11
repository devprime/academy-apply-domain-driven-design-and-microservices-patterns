namespace Domain.Aggregates.CustomerProfile;
public class CustomerProfile : AggRoot
{
    public Guid CustomerID { get; private set; }
    public string Email { get; private set; }
    public string Name { get; private set; }
    public string Photo { get; private set; }
    public DateTime BirthDate { get; private set; }
    public double Score { get; private set; }
    public CustomerProfile(Guid id, Guid customerID, string email, string name, string photo, DateTime birthDate, double score)
    {
        ID = id;
        CustomerID = customerID;
        Email = email;
        Name = name;
        Photo = photo;
        BirthDate = birthDate;
        Score = score;
    }
    public CustomerProfile()
    {
    }
    public virtual void Add()
    {
        Dp.Pipeline(Execute: () =>
        {
            ValidFields();
            ID = Guid.NewGuid();
            IsNew = true;
            var success = Dp.ProcessEvent<bool>(new CreateCustomerProfile());
        });
    }
    public virtual void Update()
    {
        Dp.Pipeline(Execute: () =>
        {
            if (ID.Equals(Guid.Empty))
                Dp.Notifications.Add("ID is required");
            ValidFields();
            var success = Dp.ProcessEvent<bool>(new UpdateCustomerProfile());
        });
    }
    public virtual void Delete()
    {
        Dp.Pipeline(Execute: () =>
        {
            if (ID != Guid.Empty)
            {
                var success = Dp.ProcessEvent<bool>(new DeleteCustomerProfile());
            }
        });
    }
    public virtual (List<CustomerProfile> Result, long Total) Get(int? limit, int? offset, string ordering, string sort, string filter)
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
                    if (filter.ToLower().StartsWith("customerid="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("email="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("name="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("photo="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("birthdate="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("score="))
                        filterIsValid = true;
                }
                if (!filterIsValid)
                    throw new PublicException($"Invalid filter '{filter}' is invalid try: 'ID', 'CustomerID', 'Email', 'Name', 'Photo', 'BirthDate', 'Score',");
            }
            var source = Dp.ProcessEvent(new CustomerProfileGet()
            {Limit = limit, Offset = offset, Ordering = ordering, Sort = sort, Filter = filter});
            return source;
        });
    }
    public virtual CustomerProfile GetByID()
    {
        var result = Dp.Pipeline(ExecuteResult: () =>
        {
            return Dp.ProcessEvent<CustomerProfile>(new CustomerProfileGetByID());
        });
        return result;
    }
    private void ValidFields()
    {
        if (String.IsNullOrWhiteSpace(Email))
            Dp.Notifications.Add("Email is required");
        if (String.IsNullOrWhiteSpace(Name))
            Dp.Notifications.Add("Name is required");
        Dp.Notifications.ValidateAndThrow();
    }
}