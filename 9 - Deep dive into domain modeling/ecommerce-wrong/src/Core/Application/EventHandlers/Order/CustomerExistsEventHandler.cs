namespace Application.EventHandlers.Order;
public class CustomerExistsEventHandler : EventHandler<CustomerExists, ICustomerState>
{
    public CustomerExistsEventHandler(ICustomerState state, IDp dp) : base(state, dp)
    {
    }

    public override dynamic Handle(CustomerExists domainEvent)
    {
        var customer = Dp.State.Customer.GetByEmail(domainEvent.Email); 
        return !customer.IsNew;
    }
}
