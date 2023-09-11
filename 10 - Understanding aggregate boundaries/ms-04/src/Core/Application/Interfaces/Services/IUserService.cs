namespace Application.Interfaces.Services;
public interface IUserService
{
    void Add(Application.Services.User.Model.User command);
    void Update(Application.Services.User.Model.User command);
    void Delete(Application.Services.User.Model.User command);
    Application.Services.User.Model.User Get(Application.Services.User.Model.User query);
    PagingResult<IList<Application.Services.User.Model.User>> GetAll(Application.Services.User.Model.User query);
}