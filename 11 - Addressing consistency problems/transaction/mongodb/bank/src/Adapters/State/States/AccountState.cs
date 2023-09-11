namespace DevPrime.State.States;
public class AccountState : IAccountState
{
    public IAccountRepository Account { get; set; }
    public AccountState(IAccountRepository account)
    {
        Account = account;
    }
}