namespace Domain.DomainServices;
public interface ICheckout : IDevPrimeDomain
{

    bool Buy(Aggregates.Order.Order order);
}