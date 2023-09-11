namespace Application.EventHandlers.PromoCode;
public class DeletePromoCodeEventHandler : EventHandler<DeletePromoCode, IPromoCodeState>
{
    public DeletePromoCodeEventHandler(IPromoCodeState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(DeletePromoCode deletePromoCode)
    {
        var promoCode = deletePromoCode.Get<Domain.Aggregates.PromoCode.PromoCode>();
        return Dp.State.PromoCode.Delete(promoCode.ID);
    }
}