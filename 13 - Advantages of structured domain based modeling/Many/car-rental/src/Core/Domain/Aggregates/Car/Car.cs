namespace Domain.Aggregates.Car;
public class Car : AggRoot
{
    public string Model { get; private set; }
    public string LicensePlate { get; private set; }
    public Car(Guid id, string model, string licensePlate)
    {
        ID = id;
        Model = model;
        LicensePlate = licensePlate;
    }
    public Car()
    {
    }
    public virtual void Add()
    {
        Dp.Pipeline(Execute: () =>
        {
            ValidFields();
            ID = Guid.NewGuid();
            IsNew = true;
            var success = Dp.ProcessEvent<bool>(new CreateCar());
        });
    }
    public virtual void Update()
    {
        Dp.Pipeline(Execute: () =>
        {
            if (ID.Equals(Guid.Empty))
                Dp.Notifications.Add("ID is required");
            ValidFields();
            var success = Dp.ProcessEvent<bool>(new UpdateCar());
        });
    }
    public virtual void Delete()
    {
        Dp.Pipeline(Execute: () =>
        {
            if (ID != Guid.Empty)
            {
                var success = Dp.ProcessEvent<bool>(new DeleteCar());
            }
        });
    }
    public virtual (List<Car> Result, long Total) Get(int? limit, int? offset, string ordering, string sort, string filter)
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
                    if (filter.ToLower().StartsWith("model="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("licenseplate="))
                        filterIsValid = true;
                }
                if (!filterIsValid)
                    throw new PublicException($"Invalid filter '{filter}' is invalid try: 'ID', 'Model', 'LicensePlate',");
            }
            var source = Dp.ProcessEvent(new CarGet()
            {Limit = limit, Offset = offset, Ordering = ordering, Sort = sort, Filter = filter});
            return source;
        });
    }
    public virtual Car GetByID()
    {
        var result = Dp.Pipeline(ExecuteResult: () =>
        {
            return Dp.ProcessEvent<Car>(new CarGetByID());
        });
        return result;
    }
    private void ValidFields()
    {
        if (String.IsNullOrWhiteSpace(Model))
            Dp.Notifications.Add("Model is required");
        if (String.IsNullOrWhiteSpace(LicensePlate))
            Dp.Notifications.Add("LicensePlate is required");
        Dp.Notifications.ValidateAndThrow();
    }
}