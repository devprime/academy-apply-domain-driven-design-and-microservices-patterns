namespace Application.EventHandlers.PromoCode;
public class PromoCodeDeletedEventHandler : EventHandler<PromoCodeDeleted, IPromoCodeState>
{
    public PromoCodeDeletedEventHandler(IPromoCodeState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(PromoCodeDeleted promoCodeDeleted)
    {
        var success = false;
        var promoCode = promoCodeDeleted.Get<Domain.Aggregates.PromoCode.PromoCode>();
        var destination = Dp.Settings.Default("stream.promocodeevents");
        var eventName = "PromoCodeDeleted";
        var eventData = new PromoCodeDeletedEventDTO()
        {ID = promoCode.ID, Code = promoCode.Code, PercentageDiscount = promoCode.PercentageDiscount, Active = promoCode.Active, ValidUntil = promoCode.ValidUntil};
        Dp.Stream.Send(destination, eventName, eventData);
        success = true;
        return success;
    }
}