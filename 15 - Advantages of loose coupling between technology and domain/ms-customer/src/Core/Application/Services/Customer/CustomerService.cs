namespace Application.Services.Customer;
public class CustomerService : ApplicationService<ICustomerState>, ICustomerService
{
    public CustomerService(ICustomerState state, IDp dp) : base(state, dp)
    {
    }
    public void Add(Model.Customer command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var customer = command.ToDomain();
            Dp.Attach(customer);
            customer.Add();
        });
    }
    public void Update(Model.Customer command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var customer = command.ToDomain();
            Dp.Attach(customer);
            customer.Update();
        });
    }
    public void Delete(Model.Customer command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var customer = command.ToDomain();
            Dp.Attach(customer);
            customer.Delete();
        });
    }
    public PagingResult<IList<Model.Customer>> GetAll(Model.Customer query)
    {
        return Dp.Pipeline(ExecuteResult: () =>
        {
            var customer = query.ToDomain();
            Dp.Attach(customer);
            var customerList = customer.Get(query.Limit, query.Offset, query.Ordering, query.Sort, query.Filter);
            var result = query.ToCustomerList(customerList.Result, customerList.Total, query.Offset, query.Limit);
            return result;
        });
    }
    public Model.Customer Get(Model.Customer query)
    {
        return Dp.Pipeline(ExecuteResult: () =>
        {
            var customer = query.ToDomain();
            Dp.Attach(customer);
            customer = customer.GetByID();
            var result = query.ToCustomer(customer);
            return result;
        });
    }
}