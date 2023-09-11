namespace DevPrime.Web.Models.Car;
public class Car
{
    public string Model { get; set; }
    public string LicensePlate { get; set; }
    public static Application.Services.Car.Model.Car ToApplication(DevPrime.Web.Models.Car.Car car)
    {
        if (car is null)
            return new Application.Services.Car.Model.Car();
        Application.Services.Car.Model.Car _car = new Application.Services.Car.Model.Car();
        _car.Model = car.Model;
        _car.LicensePlate = car.LicensePlate;
        return _car;
    }
    public static List<Application.Services.Car.Model.Car> ToApplication(IList<DevPrime.Web.Models.Car.Car> carList)
    {
        List<Application.Services.Car.Model.Car> _carList = new List<Application.Services.Car.Model.Car>();
        if (carList != null)
        {
            foreach (var car in carList)
            {
                Application.Services.Car.Model.Car _car = new Application.Services.Car.Model.Car();
                _car.Model = car.Model;
                _car.LicensePlate = car.LicensePlate;
                _carList.Add(_car);
            }
        }
        return _carList;
    }
    public virtual Application.Services.Car.Model.Car ToApplication()
    {
        var model = ToApplication(this);
        return model;
    }
}