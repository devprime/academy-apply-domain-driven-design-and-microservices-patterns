namespace DevPrime.Stream;
public class EventStream : EventStreamBase, IEventStream
{
    public override void StreamEvents()
    {
        Subscribe<ICustomerService, ImportCustomerEventDTO>("Stream1", "ImportCustomer", (dto, customerService, Dp) =>
        {
           // Dp.Stream.Idempotency.Enable = true;
            var command = new Customer()
            {
                Name = dto.Name,
                Email = dto.Email
            };
            customerService.Add(command);
        });
    }
}