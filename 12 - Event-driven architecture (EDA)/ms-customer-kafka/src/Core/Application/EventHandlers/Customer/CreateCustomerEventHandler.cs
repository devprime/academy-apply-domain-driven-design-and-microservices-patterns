namespace Application.EventHandlers.Customer;
public class CreateCustomerEventHandler : EventHandler<CreateCustomer, ICustomerState>
{
    public CreateCustomerEventHandler(ICustomerState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(CreateCustomer createCustomer)
    {
        var customer = createCustomer.Get<Domain.Aggregates.Customer.Customer>();
        return Dp.State.Customer.Add(customer);
    }
}