namespace Application.Interfaces.Services;
public interface ICustomerService
{
    void Add(Application.Services.Customer.Model.Customer command);
    void Update(Application.Services.Customer.Model.Customer command);
    void Delete(Application.Services.Customer.Model.Customer command);
    Application.Services.Customer.Model.Customer Get(Application.Services.Customer.Model.Customer query);
    PagingResult<IList<Application.Services.Customer.Model.Customer>> GetAll(Application.Services.Customer.Model.Customer query);
}