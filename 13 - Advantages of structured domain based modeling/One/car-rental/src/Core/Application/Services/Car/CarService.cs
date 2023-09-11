namespace Application.Services.Car;
public class CarService : ApplicationService<ICarState>, ICarService
{
    public CarService(ICarState state, IDp dp) : base(state, dp)
    {
    }
    public void Add(Model.Car command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var car = command.ToDomain();
            Dp.Attach(car);
            car.Add();
        });
    }
    public void Update(Model.Car command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var car = command.ToDomain();
            Dp.Attach(car);
            car.Update();
        });
    }
    public void Delete(Model.Car command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var car = command.ToDomain();
            Dp.Attach(car);
            car.Delete();
        });
    }
    public PagingResult<IList<Model.Car>> GetAll(Model.Car query)
    {
        return Dp.Pipeline(ExecuteResult: () =>
        {
            var car = query.ToDomain();
            Dp.Attach(car);
            var carList = car.Get(query.Limit, query.Offset, query.Ordering, query.Sort, query.Filter);
            var result = query.ToCarList(carList.Result, carList.Total, query.Offset, query.Limit);
            return result;
        });
    }
    public Model.Car Get(Model.Car query)
    {
        return Dp.Pipeline(ExecuteResult: () =>
        {
            var car = query.ToDomain();
            Dp.Attach(car);
            car = car.GetByID();
            var result = query.ToCar(car);
            return result;
        });
    }
}