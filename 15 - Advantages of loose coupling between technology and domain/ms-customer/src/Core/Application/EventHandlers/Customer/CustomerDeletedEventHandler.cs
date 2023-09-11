namespace Application.EventHandlers.Customer;
public class CustomerDeletedEventHandler : EventHandler<CustomerDeleted, ICustomerState>
{
    public CustomerDeletedEventHandler(ICustomerState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(CustomerDeleted customerDeleted)
    {
        var success = false;
        var customer = customerDeleted.Get<Domain.Aggregates.Customer.Customer>();
        var destination = Dp.Settings.Default("stream.customerevents");
        var eventName = "CustomerDeleted";
        var eventData = new CustomerDeletedEventDTO()
        {ID = customer.ID, Name = customer.Name, Email = customer.Email};
        Dp.Stream.Send(destination, eventName, eventData);
        success = true;
        return success;
    }
}