namespace Application.EventHandlers.Account;
public class CreateAccountEventHandler : EventHandler<CreateAccount, IAccountState>
{
    public CreateAccountEventHandler(IAccountState state, IDp dp) : base(state, dp)
    {
    }

    public override dynamic Handle(CreateAccount accountCreated)
    {
        var success = false;
        var account = accountCreated.Get<Domain.Aggregates.Account.Account>();
        Dp.State.Account.Add(account);
        
        success = true;
        return success;
    }
}