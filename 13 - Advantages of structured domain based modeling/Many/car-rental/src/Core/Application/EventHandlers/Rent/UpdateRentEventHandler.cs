namespace Application.EventHandlers.Rent;
public class UpdateRentEventHandler : EventHandler<UpdateRent, IRentState>
{
    public UpdateRentEventHandler(IRentState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(UpdateRent updateRent)
    {
        var rent = updateRent.Get<Domain.Aggregates.Rent.Rent>();
        return Dp.State.Rent.Update(rent);
    }
}