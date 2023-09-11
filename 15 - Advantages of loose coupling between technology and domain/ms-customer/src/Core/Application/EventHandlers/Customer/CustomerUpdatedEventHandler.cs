namespace Application.EventHandlers.Customer;
public class CustomerUpdatedEventHandler : EventHandler<CustomerUpdated, ICustomerState>
{
    public CustomerUpdatedEventHandler(ICustomerState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(CustomerUpdated customerUpdated)
    {
        var success = false;
        var customer = customerUpdated.Get<Domain.Aggregates.Customer.Customer>();
        var destination = Dp.Settings.Default("stream.customerevents");
        var eventName = "CustomerUpdated";
        var eventData = new CustomerUpdatedEventDTO()
        {ID = customer.ID, Name = customer.Name, Email = customer.Email};
        Dp.Stream.Send(destination, eventName, eventData);
        success = true;
        return success;
    }
}