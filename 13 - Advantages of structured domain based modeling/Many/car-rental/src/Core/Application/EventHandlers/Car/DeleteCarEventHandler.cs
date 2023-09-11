namespace Application.EventHandlers.Car;
public class DeleteCarEventHandler : EventHandler<DeleteCar, ICarState>
{
    public DeleteCarEventHandler(ICarState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(DeleteCar deleteCar)
    {
        var car = deleteCar.Get<Domain.Aggregates.Car.Car>();
        return Dp.State.Car.Delete(car.ID);
    }
}