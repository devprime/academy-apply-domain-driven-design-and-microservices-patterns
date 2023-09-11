namespace Application.EventHandlers.Order;
public class UpdateOrderEventHandler : EventHandler<UpdateOrder, IOrderState>
{
    public UpdateOrderEventHandler(IOrderState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(UpdateOrder updateOrder)
    {
        var order = updateOrder.Get<Domain.Aggregates.Order.Order>();
        return Dp.State.Order.Update(order);
    }
}