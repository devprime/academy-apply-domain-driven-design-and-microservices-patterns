namespace Application.Services.License;
public class LicenseService : ApplicationService<ILicenseState>, ILicenseService
{
    public LicenseService(ILicenseState state, IDp dp) : base(state, dp)
    {
    }
    public void Add(Model.License command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var license = command.ToDomain();
            Dp.Attach(license);
            license.Add();
        });
    }
    public void Update(Model.License command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var license = command.ToDomain();
            Dp.Attach(license);
            license.Update();
        });
    }
    public void Delete(Model.License command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var license = command.ToDomain();
            Dp.Attach(license);
            license.Delete();
        });
    }
    public PagingResult<IList<Model.License>> GetAll(Model.License query)
    {
        return Dp.Pipeline(ExecuteResult: () =>
        {
            var license = query.ToDomain();
            Dp.Attach(license);
            var licenseList = license.Get(query.Limit, query.Offset, query.Ordering, query.Sort, query.Filter);
            var result = query.ToLicenseList(licenseList.Result, licenseList.Total, query.Offset, query.Limit);
            return result;
        });
    }
    public Model.License Get(Model.License query)
    {
        return Dp.Pipeline(ExecuteResult: () =>
        {
            var license = query.ToDomain();
            Dp.Attach(license);
            license = license.GetByID();
            var result = query.ToLicense(license);
            return result;
        });
    }
}