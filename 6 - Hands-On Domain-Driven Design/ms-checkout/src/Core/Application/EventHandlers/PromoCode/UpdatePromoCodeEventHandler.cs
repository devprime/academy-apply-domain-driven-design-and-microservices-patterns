namespace Application.EventHandlers.PromoCode;
public class UpdatePromoCodeEventHandler : EventHandler<UpdatePromoCode, IPromoCodeState>
{
    public UpdatePromoCodeEventHandler(IPromoCodeState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(UpdatePromoCode updatePromoCode)
    {
        var promoCode = updatePromoCode.Get<Domain.Aggregates.PromoCode.PromoCode>();
        return Dp.State.PromoCode.Update(promoCode);
    }
}