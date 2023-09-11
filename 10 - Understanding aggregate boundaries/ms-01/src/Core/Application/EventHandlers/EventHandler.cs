using Domain.DomainEvents;
namespace Application.EventHandlers;
public class EventHandler : IEventHandler
{
    public EventHandler(IHandler handler)
    {
        handler.Add<DiscountApplied, DiscountAppliedEventHandler>();
    }
}