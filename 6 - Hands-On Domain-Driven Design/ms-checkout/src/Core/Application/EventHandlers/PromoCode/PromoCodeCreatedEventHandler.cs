namespace Application.EventHandlers.PromoCode;
public class PromoCodeCreatedEventHandler : EventHandler<PromoCodeCreated, IPromoCodeState>
{
    public PromoCodeCreatedEventHandler(IPromoCodeState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(PromoCodeCreated promoCodeCreated)
    {
        var success = false;
        var promoCode = promoCodeCreated.Get<Domain.Aggregates.PromoCode.PromoCode>();
        var destination = Dp.Settings.Default("stream.promocodeevents");
        var eventName = "PromoCodeCreated";
        var eventData = new PromoCodeCreatedEventDTO()
        {ID = promoCode.ID, Code = promoCode.Code, PercentageDiscount = promoCode.PercentageDiscount, Active = promoCode.Active, ValidUntil = promoCode.ValidUntil};
        Dp.Stream.Send(destination, eventName, eventData);
        success = true;
        return success;
    }
}