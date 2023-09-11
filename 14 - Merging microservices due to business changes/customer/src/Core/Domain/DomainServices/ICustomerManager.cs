namespace Domain.DomainServices;
public interface ICustomerManager : IDevPrimeDomain
{
    bool Register(Domain.Aggregates.Customer.Customer customer);
}