namespace Application.EventHandlers.Order;
public class OrderDeletedEventHandler : EventHandler<OrderDeleted, IOrderState>
{
    public OrderDeletedEventHandler(IOrderState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(OrderDeleted orderDeleted)
    {
        var success = false;
        var order = orderDeleted.Get<Domain.Aggregates.Order.Order>();
        var destination = Dp.Settings.Default("stream.orderevents");
        var eventName = "OrderDeleted";
        var eventData = new OrderDeletedEventDTO()
        {ID = order.ID, CustomerTaxID = order.CustomerTaxID, CustomerName = order.CustomerName, PromoCode = order.PromoCode, TotalPrice = order.TotalPrice};
        Dp.Stream.Send(destination, eventName, eventData);
        success = true;
        return success;
    }
}