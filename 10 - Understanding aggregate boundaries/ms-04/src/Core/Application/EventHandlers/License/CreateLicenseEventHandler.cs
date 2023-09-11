namespace Application.EventHandlers.License;
public class CreateLicenseEventHandler : EventHandler<CreateLicense, ILicenseState>
{
    public CreateLicenseEventHandler(ILicenseState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(CreateLicense createLicense)
    {
        var license = createLicense.Get<Domain.Aggregates.License.License>();
        return Dp.State.License.Add(license);
    }
}