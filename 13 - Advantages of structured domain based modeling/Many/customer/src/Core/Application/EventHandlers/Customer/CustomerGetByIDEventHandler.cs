namespace Application.EventHandlers.Customer;
public class CustomerGetByIDEventHandler : EventHandler<CustomerGetByID, ICustomerState>
{
    public CustomerGetByIDEventHandler(ICustomerState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(CustomerGetByID customerGetByID)
    {
        var customer = customerGetByID.Get<Domain.Aggregates.Customer.Customer>();
        return Dp.State.Customer.Get(customer.ID);
    }
}