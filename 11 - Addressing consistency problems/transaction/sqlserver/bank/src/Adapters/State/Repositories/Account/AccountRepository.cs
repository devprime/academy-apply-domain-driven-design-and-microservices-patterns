namespace DevPrime.State.Repositories.Account;
public class AccountRepository : RepositoryBase, IAccountRepository
{
    public AccountRepository(IDpState dp, ConnectionEF state) : base(dp)
    {
        ConnectionAlias = "State1";
        State = state;
    }

    #region Write

    public void Add(Domain.Aggregates.Account.Account account)
    {
        Dp.Pipeline(Execute: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            var _account = ToState(account);
            state.Account.Add(_account);
            state.SaveChanges();
        });
    }

    public void Delete(Guid accountID)
    {
        Dp.Pipeline(Execute: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            var _account = state.Account.FirstOrDefault(b => b.AccountID == accountID);
            state.Remove(_account);
            state.SaveChanges();
        });
    }

    public void Update(Domain.Aggregates.Account.Account account)
    {
        Dp.Pipeline(Execute: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            var _account = ToState(account);
            state.Update(_account);
            state.SaveChanges();
        });
    }

    #endregion Write

    #region Read

    public Domain.Aggregates.Account.Account Get(Guid accountID)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            var account = state.Account.FirstOrDefault(b => b.AccountID == accountID);
            var _account = ToDomain(account);
            return _account;
        });
    }
    public Domain.Aggregates.Account.Account GetByNumber(string number)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            var account = state.Account.FirstOrDefault(b => b.Number == number);
            var _account = ToDomain(account);
            return _account;
        });
    }
    public List<Domain.Aggregates.Account.Account> GetAll(int? limit, int? offset, string ordering, string sort, string filter)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            List<Model.Account> account = null;
            if (sort?.ToLower() == "desc")
            {
                var result = state.Account.Where(GetFilter(filter)).OrderByDescending(GetOrdering(ordering));
                if (limit != null && offset != null)
                    account = result.Skip((int)((offset - 1) * limit)).Take((int)limit).ToList();
                else
                    account = result.ToList();
            }
            else
            {
                var result = state.Account.Where(GetFilter(filter)).OrderBy(GetOrdering(ordering));
                if (limit != null && offset != null)
                    account = result.Skip((int)((offset - 1) * limit)).Take((int)limit).ToList();
                else
                    account = result.ToList();
            }
            var _account = ToDomain(account);
            return _account;
        });
    }
    private Expression<Func<Model.Account, object>> GetOrdering(string field)
    {
        Expression<Func<Model.Account, object>> exp = p => p.AccountID;
        if (!string.IsNullOrWhiteSpace(field))
        {
            if (field.ToLower() == "number")
                exp = p => p.Number;
            else if (field.ToLower() == "balance")
                exp = p => p.Balance;
            else
                exp = p => p.AccountID;
        }
        return exp;
    }
    private Expression<Func<Model.Account, bool>> GetFilter(string filter)
    {
        Expression<Func<Model.Account, bool>> exp = p => true;
        if (!string.IsNullOrWhiteSpace(filter))
        {
            var slice = filter?.Split("=");
            if (slice.Length > 1)
            {
                var field = slice[0];
                var value = slice[1];
                if (field.ToLower() == "number")
                    exp = p => p.Number.ToLower() == value.ToLower();
                else if (field.ToLower() == "balance")
                    exp = p => p.Balance == Convert.ToDouble(value);
                else
                    exp = p => true;
            }
        }
        return exp;
    }

    public bool Exists(Guid accountID)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            var account = state.Account.Where(x => x.AccountID == accountID).FirstOrDefault();
            return (accountID == account?.AccountID);
        });
    }

    public long Total(string filter)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            var total = state.Account.Where(GetFilter(filter)).Count();
            return total;
        });
    }

    #endregion Read

    #region mappers

    public static DevPrime.State.Repositories.Account.Model.Account ToState(Domain.Aggregates.Account.Account account)
    {
        if (account is null)
            return new DevPrime.State.Repositories.Account.Model.Account();
        DevPrime.State.Repositories.Account.Model.Account _account = new DevPrime.State.Repositories.Account.Model.Account();
        _account.AccountID = account.ID;
        _account.Number = account.Number;
        _account.Balance = account.Balance;
        return _account;
    }

    public static Domain.Aggregates.Account.Account ToDomain(DevPrime.State.Repositories.Account.Model.Account account)
    {
        if (account is null)
            return new Domain.Aggregates.Account.Account()
            { IsNew = true };
        Domain.Aggregates.Account.Account _account = new Domain.Aggregates.Account.Account(account.AccountID, account.Number, account.Balance);
        return _account;
    }

    public static List<Domain.Aggregates.Account.Account> ToDomain(IList<DevPrime.State.Repositories.Account.Model.Account> accountList)
    {
        List<Domain.Aggregates.Account.Account> _accountList = new List<Domain.Aggregates.Account.Account>();
        if (accountList != null)
        {
            foreach (var account in accountList)
            {
                Domain.Aggregates.Account.Account _account = new Domain.Aggregates.Account.Account(account.AccountID, account.Number, account.Balance);
                _accountList.Add(_account);
            }
        }
        return _accountList;
    }

    #endregion mappers
}