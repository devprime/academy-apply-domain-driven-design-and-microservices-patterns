namespace Application.EventHandlers.PromoCode;
public class PromoCodeGetByIDEventHandler : EventHandler<PromoCodeGetByID, IPromoCodeState>
{
    public PromoCodeGetByIDEventHandler(IPromoCodeState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(PromoCodeGetByID promoCodeGetByID)
    {
        var promoCode = promoCodeGetByID.Get<Domain.Aggregates.PromoCode.PromoCode>();
        return Dp.State.PromoCode.Get(promoCode.ID);
    }
}