namespace Application.EventHandlers.Customer;
public class CustomerCreatedEventHandler : EventHandler<CustomerCreated, ICustomerState>
{
    public CustomerCreatedEventHandler(ICustomerState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(CustomerCreated customerCreated)
    {
        var success = false;
        var customer = customerCreated.Get<Domain.Aggregates.Customer.Customer>();
        var destination = Dp.Settings.Default("stream.customerevents");
        var eventName = "ImportCustomer";
        var eventData = new CustomerCreatedEventDTO()
        {ID = customer.ID, Email = customer.Email, Name = customer.Name};
        Dp.Stream.Send(destination, eventName, eventData);
        success = true;
        return success;
    }
}