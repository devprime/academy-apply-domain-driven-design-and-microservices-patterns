namespace Application.EventHandlers.PromoCode;
public class CreatePromoCodeEventHandler : EventHandler<CreatePromoCode, IPromoCodeState>
{
    public CreatePromoCodeEventHandler(IPromoCodeState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(CreatePromoCode createPromoCode)
    {
        var promoCode = createPromoCode.Get<Domain.Aggregates.PromoCode.PromoCode>();
        return Dp.State.PromoCode.Add(promoCode);
    }
}