namespace DevPrime.Web.Models.Customer;
public class Customer
{
    public DevPrime.Web.Models.Customer.Email Email { get; set; }
    public string Name { get; set; }
    public static Application.Services.Customer.Model.Customer ToApplication(DevPrime.Web.Models.Customer.Customer customer)
    {
        if (customer is null)
            return new Application.Services.Customer.Model.Customer();
        Application.Services.Customer.Model.Customer _customer = new Application.Services.Customer.Model.Customer();
        _customer.Email = DevPrime.Web.Models.Customer.Email.ToApplication(customer.Email);
        _customer.Name = customer.Name;
        return _customer;
    }
    public static List<Application.Services.Customer.Model.Customer> ToApplication(IList<DevPrime.Web.Models.Customer.Customer> customerList)
    {
        List<Application.Services.Customer.Model.Customer> _customerList = new List<Application.Services.Customer.Model.Customer>();
        if (customerList != null)
        {
            foreach (var customer in customerList)
            {
                Application.Services.Customer.Model.Customer _customer = new Application.Services.Customer.Model.Customer();
                _customer.Email = DevPrime.Web.Models.Customer.Email.ToApplication(customer.Email);
                _customer.Name = customer.Name;
                _customerList.Add(_customer);
            }
        }
        return _customerList;
    }
    public virtual Application.Services.Customer.Model.Customer ToApplication()
    {
        var model = ToApplication(this);
        return model;
    }
}