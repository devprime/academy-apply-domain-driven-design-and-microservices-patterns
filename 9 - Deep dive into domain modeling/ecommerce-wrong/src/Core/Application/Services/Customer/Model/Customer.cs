namespace Application.Services.Customer.Model;
public class Customer
{
    internal int? Limit { get; set; }
    internal int? Offset { get; set; }
    internal string Ordering { get; set; }
    internal string Filter { get; set; }
    internal string Sort { get; set; }
    public Customer(int? limit, int? offset, string ordering, string sort, string filter)
    {
        Limit = limit;
        Offset = offset;
        Ordering = ordering;
        Filter = filter;
        Sort = sort;
    }
    public Guid ID { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public virtual PagingResult<IList<Customer>> ToCustomerList(IList<Domain.Aggregates.Customer.Customer> customerList, long? total, int? offSet, int? limit)
    {
        var _customerList = ToApplication(customerList);
        return new PagingResult<IList<Customer>>(_customerList, total, offSet, limit);
    }
    public virtual Customer ToCustomer(Domain.Aggregates.Customer.Customer customer)
    {
        var _customer = ToApplication(customer);
        return _customer;
    }
    public virtual Domain.Aggregates.Customer.Customer ToDomain()
    {
        var _customer = ToDomain(this);
        return _customer;
    }
    public virtual Domain.Aggregates.Customer.Customer ToDomain(Guid id)
    {
        var _customer = new Domain.Aggregates.Customer.Customer();
        _customer.ID = id;
        return _customer;
    }
    public Customer()
    {
    }
    public Customer(Guid id)
    {
        ID = id;
    }
    public static Application.Services.Customer.Model.Customer ToApplication(Domain.Aggregates.Customer.Customer customer)
    {
        if (customer is null)
            return new Application.Services.Customer.Model.Customer();
        Application.Services.Customer.Model.Customer _customer = new Application.Services.Customer.Model.Customer();
        _customer.ID = customer.ID;
        _customer.Email = customer.Email;
        _customer.Name = customer.Name;
        return _customer;
    }
    public static List<Application.Services.Customer.Model.Customer> ToApplication(IList<Domain.Aggregates.Customer.Customer> customerList)
    {
        List<Application.Services.Customer.Model.Customer> _customerList = new List<Application.Services.Customer.Model.Customer>();
        if (customerList != null)
        {
            foreach (var customer in customerList)
            {
                Application.Services.Customer.Model.Customer _customer = new Application.Services.Customer.Model.Customer();
                _customer.ID = customer.ID;
                _customer.Email = customer.Email;
                _customer.Name = customer.Name;
                _customerList.Add(_customer);
            }
        }
        return _customerList;
    }
    public static Domain.Aggregates.Customer.Customer ToDomain(Application.Services.Customer.Model.Customer customer)
    {
        if (customer is null)
            return new Domain.Aggregates.Customer.Customer();
        Domain.Aggregates.Customer.Customer _customer = new Domain.Aggregates.Customer.Customer(customer.ID, customer.Email, customer.Name);
        return _customer;
    }
    public static List<Domain.Aggregates.Customer.Customer> ToDomain(IList<Application.Services.Customer.Model.Customer> customerList)
    {
        List<Domain.Aggregates.Customer.Customer> _customerList = new List<Domain.Aggregates.Customer.Customer>();
        if (customerList != null)
        {
            foreach (var customer in customerList)
            {
                Domain.Aggregates.Customer.Customer _customer = new Domain.Aggregates.Customer.Customer(customer.ID, customer.Email, customer.Name);
                _customerList.Add(_customer);
            }
        }
        return _customerList;
    }
}