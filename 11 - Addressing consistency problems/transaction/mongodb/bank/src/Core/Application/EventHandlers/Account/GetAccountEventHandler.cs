namespace Application.EventHandlers.Account;
public class GetAccountEventHandler : EventHandler<GetAccount, IAccountState>
{
    public GetAccountEventHandler(IAccountState state, IDp dp) : base(state, dp)
    {
    }

    public override dynamic Handle(GetAccount domainEvent)
    {
        var source = Dp.State.Account.GetAll(domainEvent.Limit, domainEvent.Offset, domainEvent.Ordering, domainEvent.Sort, domainEvent.Filter);
        var total = Dp.State.Account.Total(domainEvent.Filter);
        return (source, total);
    }
}