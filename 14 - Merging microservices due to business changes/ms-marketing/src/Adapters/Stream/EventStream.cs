namespace DevPrime.Stream;
public class EventStream : EventStreamBase, IEventStream
{
    public override void StreamEvents()
    {
        Subscribe<ICustomerProfileService, CustomerCreatedEventDTO>("Stream1", "CustomerCreated", (dto, customerProfileService, Dp) =>
        {
            var command = new CustomerProfile()
            {
                CustomerID = dto.ID,
                Name = dto.Name,
                Email = dto.Email

            };
            customerProfileService.Add(command);
        });
    }
}