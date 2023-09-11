using Domain.DomainEvents;
namespace Application.EventHandlers;
public class DiscountAppliedEventHandler : DevPrime.Stack.Foundation.Application.EventHandler<DiscountApplied>
{
    public DiscountAppliedEventHandler(IDp dp) : base(dp)
    {
    }
    public override dynamic Handle(DiscountApplied domainEvent)
    {
        return true;
    }
}