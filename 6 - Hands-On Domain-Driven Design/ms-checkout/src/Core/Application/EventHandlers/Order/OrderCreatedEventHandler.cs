namespace Application.EventHandlers.Order;
public class OrderCreatedEventHandler : EventHandler<OrderCreated, IOrderState>
{
    public OrderCreatedEventHandler(IOrderState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(OrderCreated orderCreated)
    {
        var success = false;
        var order = orderCreated.Get<Domain.Aggregates.Order.Order>();
        var destination = Dp.Settings.Default("stream.orderevents");
        var eventName = "OrderCreated";
        var eventData = new OrderCreatedEventDTO()
        {ID = order.ID, CustomerTaxID = order.CustomerTaxID, CustomerName = order.CustomerName, PromoCode = order.PromoCode, TotalPrice = order.TotalPrice};
        Dp.Stream.Send(destination, eventName, eventData);
        success = true;
        return success;
    }
}