using System.Text.Json;

namespace DevPrime.Stream;
public class EventStream : EventStreamBase, IEventStream
{
    public override void StreamEvents()
    {
        Subscribe<ICustomerProfileService>("Stream1", "CustomerCreated", (payload, customerProfileService, Dp) =>
        {
            var dto = JsonSerializer.Deserialize<CustomerCreatedEventDTO>(payload);

            var command = new CustomerProfile()
            {
                CustomerID = dto.ID
            };
            customerProfileService.Add(command);

        });
        Subscribe<ICustomerProfileService>((dto, customerProfileService, Dp) =>
        {
            Dp.Observability.Log(dto);

        });
        Subscribe<ICustomerProfileService, CustomerCreatedEventDTO>("Stream2", "ImportCustomer", (dto, customerProfileService, Dp) =>
              {

                  var command = new CustomerProfile()
                  {
                      CustomerID = dto.ID
                  };
                  customerProfileService.Add(command);

              });

    }
}