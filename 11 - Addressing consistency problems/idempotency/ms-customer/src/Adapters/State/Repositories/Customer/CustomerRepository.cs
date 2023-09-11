namespace DevPrime.State.Repositories.Customer;
public class CustomerRepository : RepositoryBase, ICustomerRepository
{
    public CustomerRepository(IDpState dp, ConnectionEF state) : base(dp)
    {
        ConnectionAlias = "State1";
        State = state;
    }

#region Write
    public bool Add(Domain.Aggregates.Customer.Customer customer)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            var _customer = ToState(customer);
            state.Customer.Add(_customer);
            state.SaveChanges();
            return true;
        });
        if (result is null)
            return false;
        return result;
    }
    public bool Delete(Guid customerID)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            var _customer = state.Customer.FirstOrDefault(b => b.ID == customerID);
            state.Remove(_customer);
            state.SaveChanges();
            return true;
        });
        if (result is null)
            return false;
        return result;
    }
    public bool Update(Domain.Aggregates.Customer.Customer customer)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            var _customer = ToState(customer);
            state.Update(_customer);
            state.SaveChanges();
            return true;
        });
        if (result is null)
            return false;
        return result;
    }

#endregion Write

#region Read
    public Domain.Aggregates.Customer.Customer Get(Guid customerID)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            var customer = state.Customer.FirstOrDefault(b => b.ID == customerID);
            var _customer = ToDomain(customer);
            return _customer;
        });
    }
    public List<Domain.Aggregates.Customer.Customer> GetAll(int? limit, int? offset, string ordering, string sort, string filter)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            List<Model.Customer> customer = null;
            if (sort?.ToLower() == "desc")
            {
                var result = state.Customer.Where(GetFilter(filter)).OrderByDescending(GetOrdering(ordering));
                if (limit != null && offset != null)
                    customer = result.Skip(((int)offset - 1) * (int)limit).Take((int)limit).ToList();
                else
                    customer = result.ToList();
            }
            else
            {
                var result = state.Customer.Where(GetFilter(filter)).OrderBy(GetOrdering(ordering));
                if (limit != null && offset != null)
                    customer = result.Skip(((int)offset - 1) * (int)limit).Take((int)limit).ToList();
                else
                    customer = result.ToList();
            }
            var _customer = ToDomain(customer);
            return _customer;
        });
    }
    private Expression<Func<Model.Customer, object>> GetOrdering(string field)
    {
        Expression<Func<Model.Customer, object>> exp = p => p.ID;
        if (!string.IsNullOrWhiteSpace(field))
        {
            if (field.ToLower() == "name")
                exp = p => p.Name;
            else if (field.ToLower() == "email")
                exp = p => p.Email;
            else
                exp = p => p.ID;
        }
        return exp;
    }
    private Expression<Func<Model.Customer, bool>> GetFilter(string filter)
    {
        Expression<Func<Model.Customer, bool>> exp = p => true;
        if (!string.IsNullOrWhiteSpace(filter))
        {
            var slice = filter?.Split("=");
            if (slice.Length > 1)
            {
                var field = slice[0];
                var value = slice[1];
                if (string.IsNullOrWhiteSpace(value))
                {
                    exp = p => true;
                }
                else
                {
                    if (field.ToLower() == "name")
                        exp = p => p.Name.ToLower() == value.ToLower();
                    else if (field.ToLower() == "email")
                        exp = p => p.Email.ToLower() == value.ToLower();
                    else
                        exp = p => true;
                }
            }
        }
        return exp;
    }
    public bool Exists(Guid customerID)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            var customer = state.Customer.Where(x => x.ID == customerID).FirstOrDefault();
            return (customerID == customer?.ID);
        });
        if (result is null)
            return false;
        return result;
    }
    public long Total(string filter)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = (ConnectionEF)stateContext;
            var total = state.Customer.Where(GetFilter(filter)).Count();
            return total;
        });
    }

#endregion Read

#region mappers
    public static DevPrime.State.Repositories.Customer.Model.Customer ToState(Domain.Aggregates.Customer.Customer customer)
    {
        if (customer is null)
            return new DevPrime.State.Repositories.Customer.Model.Customer();
        DevPrime.State.Repositories.Customer.Model.Customer _customer = new DevPrime.State.Repositories.Customer.Model.Customer();
        _customer.ID = customer.ID;
        _customer.Name = customer.Name;
        _customer.Email = customer.Email;
        return _customer;
    }
    public static Domain.Aggregates.Customer.Customer ToDomain(DevPrime.State.Repositories.Customer.Model.Customer customer)
    {
        if (customer is null)
            return new Domain.Aggregates.Customer.Customer()
            {IsNew = true};
        Domain.Aggregates.Customer.Customer _customer = new Domain.Aggregates.Customer.Customer(customer.ID, customer.Name, customer.Email);
        return _customer;
    }
    public static List<Domain.Aggregates.Customer.Customer> ToDomain(IList<DevPrime.State.Repositories.Customer.Model.Customer> customerList)
    {
        List<Domain.Aggregates.Customer.Customer> _customerList = new List<Domain.Aggregates.Customer.Customer>();
        if (customerList != null)
        {
            foreach (var customer in customerList)
            {
                Domain.Aggregates.Customer.Customer _customer = new Domain.Aggregates.Customer.Customer(customer.ID, customer.Name, customer.Email);
                _customerList.Add(_customer);
            }
        }
        return _customerList;
    }

#endregion mappers
}