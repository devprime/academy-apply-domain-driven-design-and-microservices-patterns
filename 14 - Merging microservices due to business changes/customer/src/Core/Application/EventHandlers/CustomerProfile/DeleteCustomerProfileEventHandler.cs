namespace Application.EventHandlers.CustomerProfile;
public class DeleteCustomerProfileEventHandler : EventHandler<DeleteCustomerProfile, ICustomerProfileState>
{
    public DeleteCustomerProfileEventHandler(ICustomerProfileState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(DeleteCustomerProfile deleteCustomerProfile)
    {
        var customerProfile = deleteCustomerProfile.Get<Domain.Aggregates.CustomerProfile.CustomerProfile>();
        return Dp.State.CustomerProfile.Delete(customerProfile.ID);
    }
}