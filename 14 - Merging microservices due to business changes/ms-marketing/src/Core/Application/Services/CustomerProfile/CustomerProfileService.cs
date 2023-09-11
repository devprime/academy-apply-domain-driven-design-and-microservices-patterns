namespace Application.Services.CustomerProfile;
public class CustomerProfileService : ApplicationService<ICustomerProfileState>, ICustomerProfileService
{
    public CustomerProfileService(ICustomerProfileState state, IDp dp) : base(state, dp)
    {
    }
    public void Add(Model.CustomerProfile command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var customerProfile = command.ToDomain();
            Dp.Attach(customerProfile);
            customerProfile.Add();
        });
    }
    public void Update(Model.CustomerProfile command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var customerProfile = command.ToDomain();
            Dp.Attach(customerProfile);
            customerProfile.Update();
        });
    }
    public void Delete(Model.CustomerProfile command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var customerProfile = command.ToDomain();
            Dp.Attach(customerProfile);
            customerProfile.Delete();
        });
    }
    public PagingResult<IList<Model.CustomerProfile>> GetAll(Model.CustomerProfile query)
    {
        return Dp.Pipeline(ExecuteResult: () =>
        {
            var customerProfile = query.ToDomain();
            Dp.Attach(customerProfile);
            var customerProfileList = customerProfile.Get(query.Limit, query.Offset, query.Ordering, query.Sort, query.Filter);
            var result = query.ToCustomerProfileList(customerProfileList.Result, customerProfileList.Total, query.Offset, query.Limit);
            return result;
        });
    }
    public Model.CustomerProfile Get(Model.CustomerProfile query)
    {
        return Dp.Pipeline(ExecuteResult: () =>
        {
            var customerProfile = query.ToDomain();
            Dp.Attach(customerProfile);
            customerProfile = customerProfile.GetByID();
            var result = query.ToCustomerProfile(customerProfile);
            return result;
        });
    }
}