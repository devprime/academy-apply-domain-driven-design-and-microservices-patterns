namespace Application.EventHandlers.Car;
public class UpdateCarEventHandler : EventHandler<UpdateCar, ICarState>
{
    public UpdateCarEventHandler(ICarState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(UpdateCar updateCar)
    {
        var car = updateCar.Get<Domain.Aggregates.Car.Car>();
        return Dp.State.Car.Update(car);
    }
}