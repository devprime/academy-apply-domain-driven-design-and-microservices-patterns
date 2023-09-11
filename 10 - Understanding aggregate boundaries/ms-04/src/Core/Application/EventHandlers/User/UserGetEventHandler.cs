namespace Application.EventHandlers.User;
public class UserGetEventHandler : EventHandler<UserGet, IUserState>
{
    public UserGetEventHandler(IUserState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(UserGet domainEvent)
    {
        var source = Dp.State.User.GetAll(domainEvent.Limit, domainEvent.Offset, domainEvent.Ordering, domainEvent.Sort, domainEvent.Filter);
        var total = Dp.State.User.Total(domainEvent.Filter);
        return (source, total);
    }
}