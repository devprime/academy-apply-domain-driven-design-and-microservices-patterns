namespace Application.EventHandlers.CustomerProfile;
public class CreateCustomerProfileEventHandler : EventHandler<CreateCustomerProfile, ICustomerProfileState>
{
    public CreateCustomerProfileEventHandler(ICustomerProfileState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(CreateCustomerProfile createCustomerProfile)
    {
        var customerProfile = createCustomerProfile.Get<Domain.Aggregates.CustomerProfile.CustomerProfile>();
        return Dp.State.CustomerProfile.Add(customerProfile);
    }
}