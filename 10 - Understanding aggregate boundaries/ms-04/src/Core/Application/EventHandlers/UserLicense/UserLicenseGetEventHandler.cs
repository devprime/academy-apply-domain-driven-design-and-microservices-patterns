namespace Application.EventHandlers.UserLicense;
public class UserLicenseGetEventHandler : EventHandler<UserLicenseGet, IUserLicenseState>
{
    public UserLicenseGetEventHandler(IUserLicenseState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(UserLicenseGet domainEvent)
    {
        var source = Dp.State.UserLicense.GetAll(domainEvent.Limit, domainEvent.Offset, domainEvent.Ordering, domainEvent.Sort, domainEvent.Filter);
        var total = Dp.State.UserLicense.Total(domainEvent.Filter);
        return (source, total);
    }
}