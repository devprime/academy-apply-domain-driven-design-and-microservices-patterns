namespace Application.Interfaces.Services;
public interface IAccountService
{
    void BankTransfer(string origin, string destination, double value);
    void Add(Application.Services.Account.Model.Account command);
    void Update(Application.Services.Account.Model.Account command);
    void Delete(Application.Services.Account.Model.Account command);
    Application.Services.Account.Model.Account Get(Application.Services.Account.Model.Account query);
    PagingResult<IList<Application.Services.Account.Model.Account>> GetAll(Application.Services.Account.Model.Account query);
}