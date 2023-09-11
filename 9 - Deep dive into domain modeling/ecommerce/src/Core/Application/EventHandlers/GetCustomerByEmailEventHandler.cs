using Domain.DomainEvents;

namespace Application.EventHandlers.Order;
public class GetCustomerByEmailEventHandler : EventHandler<GetCustomerByEmail, ICustomerState>
{
    public GetCustomerByEmailEventHandler(ICustomerState state, IDp dp) : base(state, dp)
    {
    }

    public override dynamic Handle(GetCustomerByEmail domainEvent)
    {
        var customer = Dp.State.Customer.GetByEmail(domainEvent.Email); 
        return customer;
    }
}
