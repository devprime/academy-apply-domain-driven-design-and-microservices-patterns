namespace Application.EventHandlers.Account;
public class UpdateAccountEventHandler : EventHandler<UpdateAccount, IAccountState>
{
    public UpdateAccountEventHandler(IAccountState state, IDp dp) : base(state, dp)
    {
    }

    public override dynamic Handle(UpdateAccount accountUpdated)
    {
        var success = false;
        var account = accountUpdated.Get<Domain.Aggregates.Account.Account>();
        Dp.State.Account.Update(account);
        success = true;
        return success;
    }
}