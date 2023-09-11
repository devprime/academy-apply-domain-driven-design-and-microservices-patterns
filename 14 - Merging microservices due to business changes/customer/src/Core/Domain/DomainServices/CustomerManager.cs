using ServiceStack.MiniProfiler.Data;

namespace Domain.DomainServices;
public class CustomerManager : DomainService, ICustomerManager
{
    public CustomerManager(IDpDomain dp) : base(dp)
    {
    }
    public bool Register(Domain.Aggregates.Customer.Customer customer)
    {
        var result = false;
        Dp.Pipeline(Execute: () =>
        {
            customer.Add();
            var profile = new Domain.Aggregates.CustomerProfile.CustomerProfile(Guid.NewGuid(), customer.ID, customer.Email, customer.Name, string.Empty, DateTime.MinValue,0);
            profile.Add();
        });
        return result;
    }

}