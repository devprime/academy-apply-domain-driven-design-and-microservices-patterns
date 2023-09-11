namespace Application.EventHandlers.License;
public class DeleteLicenseEventHandler : EventHandler<DeleteLicense, ILicenseState>
{
    public DeleteLicenseEventHandler(ILicenseState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(DeleteLicense deleteLicense)
    {
        var license = deleteLicense.Get<Domain.Aggregates.License.License>();
        return Dp.State.License.Delete(license.ID);
    }
}