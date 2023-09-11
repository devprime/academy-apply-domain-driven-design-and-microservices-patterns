namespace Application.EventHandlers.UserLicense;
public class DeleteUserLicenseEventHandler : EventHandler<DeleteUserLicense, IUserLicenseState>
{
    public DeleteUserLicenseEventHandler(IUserLicenseState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(DeleteUserLicense deleteUserLicense)
    {
        var userLicense = deleteUserLicense.Get<Domain.Aggregates.UserLicense.UserLicense>();
        return Dp.State.UserLicense.Delete(userLicense.ID);
    }
}