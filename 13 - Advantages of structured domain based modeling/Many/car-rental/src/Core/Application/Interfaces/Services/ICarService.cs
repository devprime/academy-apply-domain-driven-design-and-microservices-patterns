namespace Application.Interfaces.Services;
public interface ICarService
{
    void Add(Application.Services.Car.Model.Car command);
    void Update(Application.Services.Car.Model.Car command);
    void Delete(Application.Services.Car.Model.Car command);
    Application.Services.Car.Model.Car Get(Application.Services.Car.Model.Car query);
    PagingResult<IList<Application.Services.Car.Model.Car>> GetAll(Application.Services.Car.Model.Car query);
}