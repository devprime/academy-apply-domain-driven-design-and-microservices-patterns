namespace Application.EventHandlers.Car;
public class CarGetByIDEventHandler : EventHandler<CarGetByID, ICarState>
{
    public CarGetByIDEventHandler(ICarState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(CarGetByID carGetByID)
    {
        var car = carGetByID.Get<Domain.Aggregates.Car.Car>();
        return Dp.State.Car.Get(car.ID);
    }
}