namespace Application.EventHandlers.Rent;
public class CreateRentEventHandler : EventHandler<CreateRent, IRentState>
{
    public CreateRentEventHandler(IRentState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(CreateRent createRent)
    {
        var rent = createRent.Get<Domain.Aggregates.Rent.Rent>();
        return Dp.State.Rent.Add(rent);
    }
}