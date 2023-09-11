namespace Application.EventHandlers.CustomerProfile;
public class GetCustomerInformationEventHandler : EventHandler<GetCustomerInformation, ICustomerProfileState>
{
    public GetCustomerInformationEventHandler(ICustomerProfileState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(GetCustomerInformation domainEvent)
    {
        var url = $"https://localhost:5001/v1/customer/{domainEvent.CustomerID}";
        var result = Dp.Services.HTTP.DpGet<GetCustomerInformationEventDTO>(url);

        return (result.Name, result.Email);
    }
}