namespace Application.EventHandlers.Rent;
public class DeleteRentEventHandler : EventHandler<DeleteRent, IRentState>
{
    public DeleteRentEventHandler(IRentState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(DeleteRent deleteRent)
    {
        var rent = deleteRent.Get<Domain.Aggregates.Rent.Rent>();
        return Dp.State.Rent.Delete(rent.ID);
    }
}