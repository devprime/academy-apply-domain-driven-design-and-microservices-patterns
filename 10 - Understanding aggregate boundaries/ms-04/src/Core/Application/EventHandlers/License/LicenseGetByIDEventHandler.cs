namespace Application.EventHandlers.License;
public class LicenseGetByIDEventHandler : EventHandler<LicenseGetByID, ILicenseState>
{
    public LicenseGetByIDEventHandler(ILicenseState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(LicenseGetByID licenseGetByID)
    {
        var license = licenseGetByID.Get<Domain.Aggregates.License.License>();
        return Dp.State.License.Get(license.ID);
    }
}