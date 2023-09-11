namespace Domain.DomainServices;
public class Checkout : DomainService, ICheckout
{
    public Checkout(IDpDomain dp) : base(dp)
    {
    }
    public bool Execute(Domain.Aggregates.Order.Order order, Domain.Aggregates.Promocode.Promocode promocode)
    {
        var result = Dp.Pipeline(ExecuteResult: () =>
        {
            if(promocode.IsActive){
              order.Add();
              promocode.Disable();
          
            }
            return true;
        });
        return result;
    }
}