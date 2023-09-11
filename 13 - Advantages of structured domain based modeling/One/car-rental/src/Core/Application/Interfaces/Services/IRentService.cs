namespace Application.Interfaces.Services;
public interface IRentService
{
    void Add(Application.Services.Rent.Model.Rent command);
    void Update(Application.Services.Rent.Model.Rent command);
    void Delete(Application.Services.Rent.Model.Rent command);
    Application.Services.Rent.Model.Rent Get(Application.Services.Rent.Model.Rent query);
    PagingResult<IList<Application.Services.Rent.Model.Rent>> GetAll(Application.Services.Rent.Model.Rent query);
}