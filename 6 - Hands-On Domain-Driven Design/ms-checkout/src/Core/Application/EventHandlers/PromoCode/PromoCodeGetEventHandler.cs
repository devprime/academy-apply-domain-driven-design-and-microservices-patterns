namespace Application.EventHandlers.PromoCode;
public class PromoCodeGetEventHandler : EventHandler<PromoCodeGet, IPromoCodeState>
{
    public PromoCodeGetEventHandler(IPromoCodeState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(PromoCodeGet domainEvent)
    {
        var source = Dp.State.PromoCode.GetAll(domainEvent.Limit, domainEvent.Offset, domainEvent.Ordering, domainEvent.Sort, domainEvent.Filter);
        var total = Dp.State.PromoCode.Total(domainEvent.Filter);
        return (source, total);
    }
}