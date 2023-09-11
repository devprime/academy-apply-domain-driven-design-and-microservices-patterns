namespace Application.EventHandlers.License;
public class UpdateLicenseEventHandler : EventHandler<UpdateLicense, ILicenseState>
{
    public UpdateLicenseEventHandler(ILicenseState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(UpdateLicense updateLicense)
    {
        var license = updateLicense.Get<Domain.Aggregates.License.License>();
        return Dp.State.License.Update(license);
    }
}