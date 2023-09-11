namespace Application.EventHandlers.PromoCode;
public class PromoCodeUpdatedEventHandler : EventHandler<PromoCodeUpdated, IPromoCodeState>
{
    public PromoCodeUpdatedEventHandler(IPromoCodeState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(PromoCodeUpdated promoCodeUpdated)
    {
        var success = false;
        var promoCode = promoCodeUpdated.Get<Domain.Aggregates.PromoCode.PromoCode>();
        var destination = Dp.Settings.Default("stream.promocodeevents");
        var eventName = "PromoCodeUpdated";
        var eventData = new PromoCodeUpdatedEventDTO()
        {ID = promoCode.ID, Code = promoCode.Code, PercentageDiscount = promoCode.PercentageDiscount, Active = promoCode.Active, ValidUntil = promoCode.ValidUntil};
        Dp.Stream.Send(destination, eventName, eventData);
        success = true;
        return success;
    }
}