namespace Application.EventHandlers.Order;
public class CustomerIsNewEventHandler : EventHandler<CustomerIsNew, IOrderState>
{
    public CustomerIsNewEventHandler(IOrderState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(CustomerIsNew customerIsNew)
    {
        var success = false;
        var order = customerIsNew.Get<Domain.Aggregates.Order.Order>();
        success = true;
        return success;
    }
}