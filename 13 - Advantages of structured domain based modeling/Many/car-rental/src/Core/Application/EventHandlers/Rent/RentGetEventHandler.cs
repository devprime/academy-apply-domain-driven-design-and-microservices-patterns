namespace Application.EventHandlers.Rent;
public class RentGetEventHandler : EventHandler<RentGet, IRentState>
{
    public RentGetEventHandler(IRentState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(RentGet domainEvent)
    {
        var source = Dp.State.Rent.GetAll(domainEvent.Limit, domainEvent.Offset, domainEvent.Ordering, domainEvent.Sort, domainEvent.Filter);
        var total = Dp.State.Rent.Total(domainEvent.Filter);
        return (source, total);
    }
}