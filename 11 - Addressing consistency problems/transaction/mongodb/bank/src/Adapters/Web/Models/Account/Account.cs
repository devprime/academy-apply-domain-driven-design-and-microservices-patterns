namespace DevPrime.Web.Models.Account;
public class Account
{
    public string Number { get; set; }
    public double Balance { get; set; }
    public static Application.Services.Account.Model.Account ToApplication(DevPrime.Web.Models.Account.Account account)
    {
        if (account is null)
            return new Application.Services.Account.Model.Account();
        Application.Services.Account.Model.Account _account = new Application.Services.Account.Model.Account();
        _account.Number = account.Number;
        _account.Balance = account.Balance;
        return _account;
    }

    public static List<Application.Services.Account.Model.Account> ToApplication(IList<DevPrime.Web.Models.Account.Account> accountList)
    {
        List<Application.Services.Account.Model.Account> _accountList = new List<Application.Services.Account.Model.Account>();
        if (accountList != null)
        {
            foreach (var account in accountList)
            {
                Application.Services.Account.Model.Account _account = new Application.Services.Account.Model.Account();
                _account.Number = account.Number;
                _account.Balance = account.Balance;
                _accountList.Add(_account);
            }
        }
        return _accountList;
    }

    public virtual Application.Services.Account.Model.Account ToApplication()
    {
        var model = ToApplication(this);
        return model;
    }
}