namespace Application.EventHandlers;
public class EventHandler : IEventHandler
{
    public EventHandler(IHandler handler)
    {
        handler.Add<CreateCustomer, CreateCustomerEventHandler>();
        handler.Add<CustomerGetByID, CustomerGetByIDEventHandler>();
        handler.Add<CustomerGet, CustomerGetEventHandler>();
        handler.Add<DeleteCustomer, DeleteCustomerEventHandler>();
        handler.Add<UpdateCustomer, UpdateCustomerEventHandler>();
        handler.Add<CreateOrder, CreateOrderEventHandler>();
        handler.Add<CustomerExists, CustomerExistsEventHandler>();
        handler.Add<CustomerIsNew, CustomerIsNewEventHandler>();
        handler.Add<DeleteOrder, DeleteOrderEventHandler>();
        handler.Add<OrderGetByID, OrderGetByIDEventHandler>();
        handler.Add<OrderGet, OrderGetEventHandler>();
        handler.Add<UpdateOrder, UpdateOrderEventHandler>();
    }
}