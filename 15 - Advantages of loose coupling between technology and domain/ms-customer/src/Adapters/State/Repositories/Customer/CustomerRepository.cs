namespace DevPrime.State.Repositories.Customer;
public class CustomerRepository : RepositoryBase, ICustomerRepository
{
    public CustomerRepository(IDpState dp) : base(dp)
    {
        ConnectionAlias = "State1";
    }

#region Write
    public bool Add(Domain.Aggregates.Customer.Customer customer)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var _customer = ToState(customer);
            state.Customer.InsertOne(_customer);
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
            var state = new ConnectionMongo(stateContext, Dp);
            state.Customer.DeleteOne(p => p.ID == customerID);
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
            var state = new ConnectionMongo(stateContext, Dp);
            var _customer = ToState(customer);
            _customer._Id = state.Customer.Find(p => p.ID == customer.ID).FirstOrDefault()._Id;
            state.Customer.ReplaceOne(p => p.ID == customer.ID, _customer);
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
            var state = new ConnectionMongo(stateContext, Dp);
            var customer = state.Customer.Find(p => p.ID == customerID).FirstOrDefault();
            var _customer = ToDomain(customer);
            return _customer;
        });
    }
    public List<Domain.Aggregates.Customer.Customer> GetAll(int? limit, int? offset, string ordering, string sort, string filter)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            List<Model.Customer> customer = null;
            if (sort?.ToLower() == "desc")
            {
                var result = state.Customer.Find(GetFilter(filter)).SortByDescending(GetOrdering(ordering));
                if (limit != null && offset != null)
                    customer = result.Skip((offset - 1) * limit).Limit(limit).ToList();
                else
                    customer = result.ToList();
            }
            else if (sort?.ToLower() == "asc")
            {
                var result = state.Customer.Find(GetFilter(filter)).SortBy(GetOrdering(ordering));
                if (limit != null && offset != null)
                    customer = result.Skip((offset - 1) * limit).Limit(limit).ToList();
                else
                    customer = result.ToList();
            }
            else
            {
                var result = state.Customer.Find(GetFilter(filter));
                if (limit != null && offset != null)
                    customer = result.Skip((offset - 1) * limit).Limit(limit).ToList();
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
    private FilterDefinition<Model.Customer> GetFilter(string filter)
    {
        var builder = Builders<Model.Customer>.Filter;
        FilterDefinition<Model.Customer> exp;
        string Name = string.Empty;
        string Email = string.Empty;
        if (!string.IsNullOrWhiteSpace(filter))
        {
            var conditions = filter.Split(",");
            if (conditions.Count() >= 1)
            {
                foreach (var condition in conditions)
                {
                    var slice = condition?.Split("=");
                    if (slice.Length > 1)
                    {
                        var field = slice[0];
                        var value = slice[1];
                        if (field.ToLower() == "name")
                            Name = value;
                        else if (field.ToLower() == "email")
                            Email = value;
                    }
                }
            }
        }
        var bfilter = builder.Empty;
        if (!string.IsNullOrWhiteSpace(Name))
        {
            var NameFilter = builder.Eq(x => x.Name, Name);
            bfilter &= NameFilter;
        }
        if (!string.IsNullOrWhiteSpace(Email))
        {
            var EmailFilter = builder.Eq(x => x.Email, Email);
            bfilter &= EmailFilter;
        }
        exp = bfilter;
        return exp;
    }
    public bool Exists(Guid customerID)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var customer = state.Customer.Find(x => x.ID == customerID).Project<Model.Customer>("{ ID: 1 }").FirstOrDefault();
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
            var state = new ConnectionMongo(stateContext, Dp);
            var total = state.Customer.Find(GetFilter(filter)).CountDocuments();
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