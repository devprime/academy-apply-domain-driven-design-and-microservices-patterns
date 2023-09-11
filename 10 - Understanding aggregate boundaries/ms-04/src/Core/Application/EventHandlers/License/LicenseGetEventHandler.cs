namespace Application.EventHandlers.License;
public class LicenseGetEventHandler : EventHandler<LicenseGet, ILicenseState>
{
    public LicenseGetEventHandler(ILicenseState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(LicenseGet domainEvent)
    {
        var source = Dp.State.License.GetAll(domainEvent.Limit, domainEvent.Offset, domainEvent.Ordering, domainEvent.Sort, domainEvent.Filter);
        var total = Dp.State.License.Total(domainEvent.Filter);
        return (source, total);
    }
}