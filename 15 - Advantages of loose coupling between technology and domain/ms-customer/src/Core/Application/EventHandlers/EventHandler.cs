namespace Application.EventHandlers;
public class EventHandler : IEventHandler
{
    public EventHandler(IHandler handler)
    {
        handler.Add<CreateCustomer, CreateCustomerEventHandler>();
        handler.Add<CustomerCreated, CustomerCreatedEventHandler>();
        handler.Add<CustomerDeleted, CustomerDeletedEventHandler>();
        handler.Add<CustomerGetByID, CustomerGetByIDEventHandler>();
        handler.Add<CustomerGet, CustomerGetEventHandler>();
        handler.Add<CustomerUpdated, CustomerUpdatedEventHandler>();
        handler.Add<DeleteCustomer, DeleteCustomerEventHandler>();
        handler.Add<UpdateCustomer, UpdateCustomerEventHandler>();
    }
}