using Domain.DomainEvents;
namespace Application.EventHandlers;
public class GetPromoCodeByCodeEventHandler : DevPrime.Stack.Foundation.Application.EventHandler<GetPromoCodeByCode, IPromoCodeState>
{
    public GetPromoCodeByCodeEventHandler(IPromoCodeState state, IDp dp) : base(state, dp)
    {

    }
    public override dynamic Handle(GetPromoCodeByCode domainEvent)
    {
        var order = domainEvent.Get<Domain.Aggregates.Order.Order>();
        var promocode = Dp.State.PromoCode.GetByCode(order.PromoCode);
        return promocode;
    }
}