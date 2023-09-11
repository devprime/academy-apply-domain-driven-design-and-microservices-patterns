namespace Application.Interfaces.Services;
public interface ILicenseService
{
    void Add(Application.Services.License.Model.License command);
    void Update(Application.Services.License.Model.License command);
    void Delete(Application.Services.License.Model.License command);
    Application.Services.License.Model.License Get(Application.Services.License.Model.License query);
    PagingResult<IList<Application.Services.License.Model.License>> GetAll(Application.Services.License.Model.License query);
}