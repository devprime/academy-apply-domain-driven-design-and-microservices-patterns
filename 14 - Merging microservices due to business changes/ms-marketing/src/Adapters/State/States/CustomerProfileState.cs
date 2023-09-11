namespace DevPrime.State.States;
public class CustomerProfileState : ICustomerProfileState
{
    public ICustomerProfileRepository CustomerProfile { get; set; }
    public CustomerProfileState(ICustomerProfileRepository customerProfile)
    {
        CustomerProfile = customerProfile;
    }
}