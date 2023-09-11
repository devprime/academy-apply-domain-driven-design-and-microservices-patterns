namespace Application.EventHandlers.CustomerProfile;
public class UpdateCustomerProfileEventHandler : EventHandler<UpdateCustomerProfile, ICustomerProfileState>
{
    public UpdateCustomerProfileEventHandler(ICustomerProfileState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(UpdateCustomerProfile updateCustomerProfile)
    {
        var customerProfile = updateCustomerProfile.Get<Domain.Aggregates.CustomerProfile.CustomerProfile>();
        return Dp.State.CustomerProfile.Update(customerProfile);
    }
}