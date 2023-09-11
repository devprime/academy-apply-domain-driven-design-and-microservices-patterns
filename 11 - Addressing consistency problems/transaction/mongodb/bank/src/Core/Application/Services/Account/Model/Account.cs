namespace Application.Services.Account.Model;
public class Account
{
    internal int? Limit { get; set; }
    internal int? Offset { get; set; }
    internal string Ordering { get; set; }
    internal string Filter { get; set; }
    internal string Sort { get; set; }
    public Account(int? limit, int? offset, string ordering, string sort, string filter)
    {
        Limit = limit;
        Offset = offset;
        Ordering = ordering;
        Filter = filter;
        Sort = sort;
    }

    public Guid ID { get; set; }
    public string Number { get; set; }
    public double Balance { get; set; }
    public virtual PagingResult<IList<Account>> ToAccountList(IList<Domain.Aggregates.Account.Account> accountList, long? total, int? offSet, int? limit)
    {
        var _accountList = ToApplication(accountList);
        return new PagingResult<IList<Account>>(_accountList, total, offSet, limit);
    }

    public virtual Account ToAccount(Domain.Aggregates.Account.Account account)
    {
        var _account = ToApplication(account);
        return _account;
    }

    public virtual Domain.Aggregates.Account.Account ToDomain()
    {
        var _account = ToDomain(this);
        return _account;
    }

    public virtual Domain.Aggregates.Account.Account ToDomain(Guid id)
    {
        var _account = new Domain.Aggregates.Account.Account();
        _account.ID = id;
        return _account;
    }

    public Account()
    {
    }

    public Account(Guid id)
    {
        ID = id;
    }

    public static Application.Services.Account.Model.Account ToApplication(Domain.Aggregates.Account.Account account)
    {
        if (account is null)
            return new Application.Services.Account.Model.Account();
        Application.Services.Account.Model.Account _account = new Application.Services.Account.Model.Account();
        _account.ID = account.ID;
        _account.Number = account.Number;
        _account.Balance = account.Balance;
        return _account;
    }

    public static List<Application.Services.Account.Model.Account> ToApplication(IList<Domain.Aggregates.Account.Account> accountList)
    {
        List<Application.Services.Account.Model.Account> _accountList = new List<Application.Services.Account.Model.Account>();
        if (accountList != null)
        {
            foreach (var account in accountList)
            {
                Application.Services.Account.Model.Account _account = new Application.Services.Account.Model.Account();
                _account.ID = account.ID;
                _account.Number = account.Number;
                _account.Balance = account.Balance;
                _accountList.Add(_account);
            }
        }
        return _accountList;
    }

    public static Domain.Aggregates.Account.Account ToDomain(Application.Services.Account.Model.Account account)
    {
        if (account is null)
            return new Domain.Aggregates.Account.Account();
        Domain.Aggregates.Account.Account _account = new Domain.Aggregates.Account.Account(account.ID, account.Number, account.Balance);
        return _account;
    }

    public static List<Domain.Aggregates.Account.Account> ToDomain(IList<Application.Services.Account.Model.Account> accountList)
    {
        List<Domain.Aggregates.Account.Account> _accountList = new List<Domain.Aggregates.Account.Account>();
        if (accountList != null)
        {
            foreach (var account in accountList)
            {
                Domain.Aggregates.Account.Account _account = new Domain.Aggregates.Account.Account(account.ID, account.Number, account.Balance);
                _accountList.Add(_account);
            }
        }
        return _accountList;
    }
}