namespace Application.EventHandlers.UserLicense;
public class UserLicenseGetByIDEventHandler : EventHandler<UserLicenseGetByID, IUserLicenseState>
{
    public UserLicenseGetByIDEventHandler(IUserLicenseState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(UserLicenseGetByID userLicenseGetByID)
    {
        var userLicense = userLicenseGetByID.Get<Domain.Aggregates.UserLicense.UserLicense>();
        return Dp.State.UserLicense.Get(userLicense.ID);
    }
}