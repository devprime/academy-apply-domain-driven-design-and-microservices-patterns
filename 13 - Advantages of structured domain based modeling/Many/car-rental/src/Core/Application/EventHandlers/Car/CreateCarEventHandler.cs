namespace Application.EventHandlers.Car;
public class CreateCarEventHandler : EventHandler<CreateCar, ICarState>
{
    public CreateCarEventHandler(ICarState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(CreateCar createCar)
    {
        var car = createCar.Get<Domain.Aggregates.Car.Car>();
        return Dp.State.Car.Add(car);
    }
}