namespace Application.Services.Account;
using  Domain.DomainServices;
public class AccountService : ApplicationService<IAccountState>, IAccountService
{
    protected ITransfer Transfer { get; set;}
    public AccountService(IAccountState state, IDp dp, ITransfer transfer ) : base(state, dp)
    {
        Transfer = transfer; 
    }
    public void BankTransfer(string origin, string destination, double value)
    {
        Dp.Pipeline(Execute: () =>
        {
            var from = Dp.State.Account.GetByNumber(origin);
            var to = Dp.State.Account.GetByNumber(destination);

            Dp.Attach(Transfer);
            Dp.Attach(from);
            Dp.Attach(to);

            Transfer.BankTransfer(from, to, value);
        });

    }
    public void Add(Model.Account command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var account = command.ToDomain();
            Dp.Attach(account);
            account.Add();
        });
    }

    public void Update(Model.Account command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var account = command.ToDomain();
            Dp.Attach(account);
            account.Update();
        });
    }

    public void Delete(Model.Account command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var account = command.ToDomain();
            Dp.Attach(account);
            account.Delete();
        });
    }

    public PagingResult<IList<Model.Account>> GetAll(Model.Account query)
    {
        return Dp.Pipeline(ExecuteResult: () =>
        {
            var account = query.ToDomain();
            Dp.Attach(account);
            var accountList = account.Get(query.Limit, query.Offset, query.Ordering, query.Sort, query.Filter);
            var result = query.ToAccountList(accountList.Result, accountList.Total, query.Offset, query.Limit);
            return result;
        });
    }

    public Model.Account Get(Model.Account query)
    {
        return Dp.Pipeline(ExecuteResult: () =>
        {
            var account = query.ToAccount(Dp.State.Account.Get(query.ID));
            return account;
        });
    }
}