namespace Application.Interfaces.Services;
public interface IUserLicenseService
{
    void Add(Application.Services.UserLicense.Model.UserLicense command);
    void Update(Application.Services.UserLicense.Model.UserLicense command);
    void Delete(Application.Services.UserLicense.Model.UserLicense command);
    Application.Services.UserLicense.Model.UserLicense Get(Application.Services.UserLicense.Model.UserLicense query);
    PagingResult<IList<Application.Services.UserLicense.Model.UserLicense>> GetAll(Application.Services.UserLicense.Model.UserLicense query);
}