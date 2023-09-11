namespace Application.Interfaces.Adapters.State;
public interface ICustomerProfileState
{
    ICustomerProfileRepository CustomerProfile { get; set; }
}