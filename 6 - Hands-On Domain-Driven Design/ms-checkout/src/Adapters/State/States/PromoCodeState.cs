namespace DevPrime.State.States;
public class PromoCodeState : IPromoCodeState
{
    public IPromoCodeRepository PromoCode { get; set; }
    public PromoCodeState(IPromoCodeRepository promoCode)
    {
        PromoCode = promoCode;
    }
}