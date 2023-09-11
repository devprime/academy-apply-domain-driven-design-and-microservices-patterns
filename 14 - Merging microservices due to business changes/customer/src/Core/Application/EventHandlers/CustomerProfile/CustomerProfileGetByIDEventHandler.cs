namespace Application.EventHandlers.CustomerProfile;
public class CustomerProfileGetByIDEventHandler : EventHandler<CustomerProfileGetByID, ICustomerProfileState>
{
    public CustomerProfileGetByIDEventHandler(ICustomerProfileState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(CustomerProfileGetByID customerProfileGetByID)
    {
        var customerProfile = customerProfileGetByID.Get<Domain.Aggregates.CustomerProfile.CustomerProfile>();
        return Dp.State.CustomerProfile.Get(customerProfile.ID);
    }
}