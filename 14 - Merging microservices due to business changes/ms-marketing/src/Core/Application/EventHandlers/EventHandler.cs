namespace Application.EventHandlers;
public class EventHandler : IEventHandler
{
    public EventHandler(IHandler handler)
    {
        handler.Add<CreateCustomerProfile, CreateCustomerProfileEventHandler>();
        handler.Add<CustomerProfileGetByID, CustomerProfileGetByIDEventHandler>();
        handler.Add<CustomerProfileGet, CustomerProfileGetEventHandler>();
        handler.Add<DeleteCustomerProfile, DeleteCustomerProfileEventHandler>();
        handler.Add<UpdateCustomerProfile, UpdateCustomerProfileEventHandler>();
    }
}