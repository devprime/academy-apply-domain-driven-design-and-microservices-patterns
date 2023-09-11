namespace Application.EventHandlers.Rent;
public class RentGetByIDEventHandler : EventHandler<RentGetByID, IRentState>
{
    public RentGetByIDEventHandler(IRentState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(RentGetByID rentGetByID)
    {
        var rent = rentGetByID.Get<Domain.Aggregates.Rent.Rent>();
        return Dp.State.Rent.Get(rent.ID);
    }
}