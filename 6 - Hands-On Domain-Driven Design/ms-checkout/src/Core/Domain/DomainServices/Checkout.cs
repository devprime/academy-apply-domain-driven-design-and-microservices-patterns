namespace Domain.DomainServices;
public class Checkout : DomainService, ICheckout
{
    public Checkout(IDpDomain dp) : base(dp)
    {
    }
    public bool Buy(Order order)
    {
        var result = Dp.Pipeline(ExecuteResult: () =>
        {
            var getPromoCodeByCode = new GetPromoCodeByCode();
            getPromoCodeByCode.Attach(order);
            var promocode = Dp.ProcessEvent<PromoCode>(getPromoCodeByCode); 
            Dp.Attach(promocode);
            if(!promocode.Active)
               throw new PublicException("Invalid promocode!");                     
 
            promocode.Disable();
            order.SetDiscount(promocode.PercentageDiscount);

            promocode.Update();
            order.Add();
            return true;
        });
        return result;
    }

}