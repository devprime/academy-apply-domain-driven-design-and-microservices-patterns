namespace Domain.DomainServices;
public interface ICheckout : IDevPrimeDomain
{
    bool Buy(Order order);
}