namespace Application.Interfaces.Services;
public interface ICustomerProfileService
{
    void Add(Application.Services.CustomerProfile.Model.CustomerProfile command);
    void Update(Application.Services.CustomerProfile.Model.CustomerProfile command);
    void Delete(Application.Services.CustomerProfile.Model.CustomerProfile command);
    Application.Services.CustomerProfile.Model.CustomerProfile Get(Application.Services.CustomerProfile.Model.CustomerProfile query);
    PagingResult<IList<Application.Services.CustomerProfile.Model.CustomerProfile>> GetAll(Application.Services.CustomerProfile.Model.CustomerProfile query);
}