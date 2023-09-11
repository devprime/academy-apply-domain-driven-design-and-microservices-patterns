namespace DevPrime.Stream;
public class EventStream : EventStreamBase, IEventStream
{
    public override void StreamEvents()
    {
        Subscribe<IOrderService, OrderPaidEventDTO>("Stream1", "OrderPaid", (dto, orderService, Dp) =>
        {
            var command = new Order()
            {
                ID = dto.ID
            };
            orderService.Update(command);

        });
    }
}