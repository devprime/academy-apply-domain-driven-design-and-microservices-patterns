namespace Application.Services.Car.Model;
public class Car
{
    internal int? Limit { get; set; }
    internal int? Offset { get; set; }
    internal string Ordering { get; set; }
    internal string Filter { get; set; }
    internal string Sort { get; set; }
    public Car(int? limit, int? offset, string ordering, string sort, string filter)
    {
        Limit = limit;
        Offset = offset;
        Ordering = ordering;
        Filter = filter;
        Sort = sort;
    }
    public Guid ID { get; set; }
    public string Model { get; set; }
    public string LicensePlate { get; set; }
    public virtual PagingResult<IList<Car>> ToCarList(IList<Domain.Aggregates.Car.Car> carList, long? total, int? offSet, int? limit)
    {
        var _carList = ToApplication(carList);
        return new PagingResult<IList<Car>>(_carList, total, offSet, limit);
    }
    public virtual Car ToCar(Domain.Aggregates.Car.Car car)
    {
        var _car = ToApplication(car);
        return _car;
    }
    public virtual Domain.Aggregates.Car.Car ToDomain()
    {
        var _car = ToDomain(this);
        return _car;
    }
    public virtual Domain.Aggregates.Car.Car ToDomain(Guid id)
    {
        var _car = new Domain.Aggregates.Car.Car();
        _car.ID = id;
        return _car;
    }
    public Car()
    {
    }
    public Car(Guid id)
    {
        ID = id;
    }
    public static Application.Services.Car.Model.Car ToApplication(Domain.Aggregates.Car.Car car)
    {
        if (car is null)
            return new Application.Services.Car.Model.Car();
        Application.Services.Car.Model.Car _car = new Application.Services.Car.Model.Car();
        _car.ID = car.ID;
        _car.Model = car.Model;
        _car.LicensePlate = car.LicensePlate;
        return _car;
    }
    public static List<Application.Services.Car.Model.Car> ToApplication(IList<Domain.Aggregates.Car.Car> carList)
    {
        List<Application.Services.Car.Model.Car> _carList = new List<Application.Services.Car.Model.Car>();
        if (carList != null)
        {
            foreach (var car in carList)
            {
                Application.Services.Car.Model.Car _car = new Application.Services.Car.Model.Car();
                _car.ID = car.ID;
                _car.Model = car.Model;
                _car.LicensePlate = car.LicensePlate;
                _carList.Add(_car);
            }
        }
        return _carList;
    }
    public static Domain.Aggregates.Car.Car ToDomain(Application.Services.Car.Model.Car car)
    {
        if (car is null)
            return new Domain.Aggregates.Car.Car();
        Domain.Aggregates.Car.Car _car = new Domain.Aggregates.Car.Car(car.ID, car.Model, car.LicensePlate);
        return _car;
    }
    public static List<Domain.Aggregates.Car.Car> ToDomain(IList<Application.Services.Car.Model.Car> carList)
    {
        List<Domain.Aggregates.Car.Car> _carList = new List<Domain.Aggregates.Car.Car>();
        if (carList != null)
        {
            foreach (var car in carList)
            {
                Domain.Aggregates.Car.Car _car = new Domain.Aggregates.Car.Car(car.ID, car.Model, car.LicensePlate);
                _carList.Add(_car);
            }
        }
        return _carList;
    }
}