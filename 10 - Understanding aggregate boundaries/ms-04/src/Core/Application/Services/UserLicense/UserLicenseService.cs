namespace Application.Services.UserLicense;
public class UserLicenseService : ApplicationService<IUserLicenseState>, IUserLicenseService
{
    public UserLicenseService(IUserLicenseState state, IDp dp) : base(state, dp)
    {
    }
    public void Add(Model.UserLicense command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var userLicense = command.ToDomain();
            Dp.Attach(userLicense);
            userLicense.Add();
        });
    }
    public void Update(Model.UserLicense command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var userLicense = command.ToDomain();
            Dp.Attach(userLicense);
            userLicense.Update();
        });
    }
    public void Delete(Model.UserLicense command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var userLicense = command.ToDomain();
            Dp.Attach(userLicense);
            userLicense.Delete();
        });
    }
    public PagingResult<IList<Model.UserLicense>> GetAll(Model.UserLicense query)
    {
        return Dp.Pipeline(ExecuteResult: () =>
        {
            var userLicense = query.ToDomain();
            Dp.Attach(userLicense);
            var userLicenseList = userLicense.Get(query.Limit, query.Offset, query.Ordering, query.Sort, query.Filter);
            var result = query.ToUserLicenseList(userLicenseList.Result, userLicenseList.Total, query.Offset, query.Limit);
            return result;
        });
    }
    public Model.UserLicense Get(Model.UserLicense query)
    {
        return Dp.Pipeline(ExecuteResult: () =>
        {
            var userLicense = query.ToDomain();
            Dp.Attach(userLicense);
            userLicense = userLicense.GetByID();
            var result = query.ToUserLicense(userLicense);
            return result;
        });
    }
}