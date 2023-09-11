namespace Application.EventHandlers.UserLicense;
public class UpdateUserLicenseEventHandler : EventHandler<UpdateUserLicense, IUserLicenseState>
{
    public UpdateUserLicenseEventHandler(IUserLicenseState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(UpdateUserLicense updateUserLicense)
    {
        var userLicense = updateUserLicense.Get<Domain.Aggregates.UserLicense.UserLicense>();
        return Dp.State.UserLicense.Update(userLicense);
    }
}