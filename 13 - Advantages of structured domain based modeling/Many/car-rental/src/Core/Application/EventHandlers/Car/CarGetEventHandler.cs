namespace Application.EventHandlers.Car;
public class CarGetEventHandler : EventHandler<CarGet, ICarState>
{
    public CarGetEventHandler(ICarState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(CarGet domainEvent)
    {
        var source = Dp.State.Car.GetAll(domainEvent.Limit, domainEvent.Offset, domainEvent.Ordering, domainEvent.Sort, domainEvent.Filter);
        var total = Dp.State.Car.Total(domainEvent.Filter);
        return (source, total);
    }
}