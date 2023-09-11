namespace Application.EventHandlers.UserLicense;
public class LicenseExistsEventHandler : EventHandler<LicenseExists, ILicenseState>
{
    public LicenseExistsEventHandler(ILicenseState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(LicenseExists licenseExists)
    {
        var id = licenseExists.LicenseID;
        var result = Dp.State.License.Exists(id);
        return result;
    }
}