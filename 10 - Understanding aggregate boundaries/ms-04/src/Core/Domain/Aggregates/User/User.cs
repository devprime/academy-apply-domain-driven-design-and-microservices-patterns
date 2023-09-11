namespace Domain.Aggregates.User;
public class User : AggRoot
{
    public string Name { get; private set; }
    public User(Guid id, string name)
    {
        ID = id;
        Name = name;
    }
    public User()
    {
    }
    public virtual void Add()
    {
        Dp.Pipeline(Execute: () =>
        {
            ValidFields();
            ID = Guid.NewGuid();
            IsNew = true;
            var success = Dp.ProcessEvent<bool>(new CreateUser());
        });
    }
    public virtual void Update()
    {
        Dp.Pipeline(Execute: () =>
        {
            if (ID.Equals(Guid.Empty))
                Dp.Notifications.Add("ID is required");
            ValidFields();
            var success = Dp.ProcessEvent<bool>(new UpdateUser());
        });
    }
    public virtual void Delete()
    {
        Dp.Pipeline(Execute: () =>
        {
            if (ID != Guid.Empty)
            {
                var success = Dp.ProcessEvent<bool>(new DeleteUser());
            }
        });
    }
    public virtual (List<User> Result, long Total) Get(int? limit, int? offset, string ordering, string sort, string filter)
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
            var source = Dp.ProcessEvent(new UserGet()
            {Limit = limit, Offset = offset, Ordering = ordering, Sort = sort, Filter = filter});
            return source;
        });
    }
    public virtual User GetByID()
    {
        var result = Dp.Pipeline(ExecuteResult: () =>
        {
            return Dp.ProcessEvent<User>(new UserGetByID());
        });
        return result;
    }
    private void ValidFields()
    {
        if (String.IsNullOrWhiteSpace(Name))
            Dp.Notifications.Add("Name is required");
        Dp.Notifications.ValidateAndThrow();
    }
}