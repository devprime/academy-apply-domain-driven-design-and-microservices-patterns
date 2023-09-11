namespace Application.EventHandlers.UserLicense;
public class CreateUserLicenseEventHandler : EventHandler<CreateUserLicense, IUserLicenseState>
{
    public CreateUserLicenseEventHandler(IUserLicenseState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(CreateUserLicense createUserLicense)
    {
        var userLicense = createUserLicense.Get<Domain.Aggregates.UserLicense.UserLicense>();
        return Dp.State.UserLicense.Add(userLicense);
    }
}