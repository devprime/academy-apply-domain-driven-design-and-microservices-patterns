namespace Domain.Aggregates.Rent;
public class Rent : AggRoot
{
    public string LicensePlate { get; private set; }
    public string TaxID { get; private set; }
    public DateTime Start { get; private set; }
    public DateTime End { get; private set; }
    public Rent(Guid id, string licensePlate, string taxID, DateTime start, DateTime end)
    {
        ID = id;
        LicensePlate = licensePlate;
        TaxID = taxID;
        Start = start;
        End = end;
    }
    public Rent()
    {
    }
    public virtual void Add()
    {
        Dp.Pipeline(Execute: () =>
        {
            ValidFields();
            ID = Guid.NewGuid();
            IsNew = true;
            var success = Dp.ProcessEvent<bool>(new CreateRent());
        });
    }
    public virtual void Update()
    {
        Dp.Pipeline(Execute: () =>
        {
            if (ID.Equals(Guid.Empty))
                Dp.Notifications.Add("ID is required");
            ValidFields();
            var success = Dp.ProcessEvent<bool>(new UpdateRent());
        });
    }
    public virtual void Delete()
    {
        Dp.Pipeline(Execute: () =>
        {
            if (ID != Guid.Empty)
            {
                var success = Dp.ProcessEvent<bool>(new DeleteRent());
            }
        });
    }
    public virtual (List<Rent> Result, long Total) Get(int? limit, int? offset, string ordering, string sort, string filter)
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
                    if (filter.ToLower().StartsWith("licenseplate="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("taxid="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("start="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("end="))
                        filterIsValid = true;
                }
                if (!filterIsValid)
                    throw new PublicException($"Invalid filter '{filter}' is invalid try: 'ID', 'LicensePlate', 'TaxID', 'Start', 'End',");
            }
            var source = Dp.ProcessEvent(new RentGet()
            {Limit = limit, Offset = offset, Ordering = ordering, Sort = sort, Filter = filter});
            return source;
        });
    }
    public virtual Rent GetByID()
    {
        var result = Dp.Pipeline(ExecuteResult: () =>
        {
            return Dp.ProcessEvent<Rent>(new RentGetByID());
        });
        return result;
    }
    private void ValidFields()
    {
        if (String.IsNullOrWhiteSpace(LicensePlate))
            Dp.Notifications.Add("LicensePlate is required");
        if (String.IsNullOrWhiteSpace(TaxID))
            Dp.Notifications.Add("TaxID is required");
        if (Start == DateTime.MinValue)
            Dp.Notifications.Add("Start is required");
        if (End == DateTime.MinValue)
            Dp.Notifications.Add("End is required");
        Dp.Notifications.ValidateAndThrow();
    }
}