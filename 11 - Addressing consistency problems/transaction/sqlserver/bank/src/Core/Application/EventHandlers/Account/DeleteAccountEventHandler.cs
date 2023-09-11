namespace Application.EventHandlers.Account;
public class DeleteAccountEventHandler : EventHandler<DeleteAccount, IAccountState>
{
    public DeleteAccountEventHandler(IAccountState state, IDp dp) : base(state, dp)
    {
    }

    public override dynamic Handle(DeleteAccount accountDeleted)
    {
        var success = false;
        var account = accountDeleted.Get<Domain.Aggregates.Account.Account>();
        Dp.State.Account.Delete(account.ID);
        success = true;
        return success;
    }
}