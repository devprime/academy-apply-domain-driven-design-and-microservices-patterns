namespace Domain.Aggregates.License;
public class License : AggRoot
{
    public string Description { get; private set; }
    public LicenseType Type { get; private set; }
    public License(Guid id, string description, string type)
    {
        ID = id;
        Description = description;
        typeValue = type;
    }
    public License()
    {
    }
    public virtual void Add()
    {
        Dp.Pipeline(Execute: () =>
        {
            Type = TypeParse.GetTypeValue(typeValue);
            ValidFields();
            ID = Guid.NewGuid();
            IsNew = true;
            var success = Dp.ProcessEvent<bool>(new CreateLicense());
        });
    }
    public virtual void Update()
    {
        Dp.Pipeline(Execute: () =>
        {
            Type = TypeParse.GetTypeValue(typeValue);
            if (ID.Equals(Guid.Empty))
                Dp.Notifications.Add("ID is required");
            ValidFields();
            var success = Dp.ProcessEvent<bool>(new UpdateLicense());
        });
    }
    public virtual void Delete()
    {
        Dp.Pipeline(Execute: () =>
        {
            if (ID != Guid.Empty)
            {
                var success = Dp.ProcessEvent<bool>(new DeleteLicense());
            }
        });
    }
    public virtual (List<License> Result, long Total) Get(int? limit, int? offset, string ordering, string sort, string filter)
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
                    if (filter.ToLower().StartsWith("description="))
                        filterIsValid = true;
                }
                if (!filterIsValid)
                    throw new PublicException($"Invalid filter '{filter}' is invalid try: 'ID', 'Description',");
            }
            var source = Dp.ProcessEvent(new LicenseGet()
            {Limit = limit, Offset = offset, Ordering = ordering, Sort = sort, Filter = filter});
            return source;
        });
    }
    public virtual License GetByID()
    {
        var result = Dp.Pipeline(ExecuteResult: () =>
        {
            return Dp.ProcessEvent<License>(new LicenseGetByID());
        });
        return result;
    }
    private void ValidFields()
    {
        if (String.IsNullOrWhiteSpace(Description))
            Dp.Notifications.Add("Description is required");
        if (String.IsNullOrWhiteSpace(typeValue))
            Dp.Notifications.Add("Type is required");
        Dp.Notifications.ValidateAndThrow();
    }
    private string typeValue { get; set; }
}