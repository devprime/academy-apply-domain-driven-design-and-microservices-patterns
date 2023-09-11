using Domain.DomainEvents;

namespace Domain.DomainServices;
public class Checkout : DomainService, ICheckout
{
    public Checkout(IDpDomain dp) : base(dp)
    {
    }
    public bool Buy(Aggregates.Order.Order order)
    {
        var result = Dp.Pipeline(ExecuteResult: () =>
        {
            var customer = Dp.ProcessEvent<Aggregates.Customer.Customer>(new GetCustomerByEmail(order.CustomerEmail));
            if (customer.IsNew)
            {
              customer.SetName(order.CustomerName);
              customer.SetEmail(order.CustomerEmail);
              Dp.Attach(customer);
              customer.Add();   
            }
            Dp.Attach(order);
            order.Add();
            return true;
        });
        return result;
    }
}