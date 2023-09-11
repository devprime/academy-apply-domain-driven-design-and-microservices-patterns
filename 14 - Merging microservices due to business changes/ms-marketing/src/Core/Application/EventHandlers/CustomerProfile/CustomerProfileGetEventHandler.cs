namespace Application.EventHandlers.CustomerProfile;
public class CustomerProfileGetEventHandler : EventHandler<CustomerProfileGet, ICustomerProfileState>
{
    public CustomerProfileGetEventHandler(ICustomerProfileState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(CustomerProfileGet domainEvent)
    {
        var source = Dp.State.CustomerProfile.GetAll(domainEvent.Limit, domainEvent.Offset, domainEvent.Ordering, domainEvent.Sort, domainEvent.Filter);
        var total = Dp.State.CustomerProfile.Total(domainEvent.Filter);
        return (source, total);
    }
}