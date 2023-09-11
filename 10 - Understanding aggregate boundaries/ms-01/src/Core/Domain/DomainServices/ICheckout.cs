namespace Domain.DomainServices;
public interface ICheckout : IDevPrimeDomain
{
    bool Execute(Domain.Aggregates.Order.Order order, Domain.Aggregates.Promocode.Promocode promocode);
}