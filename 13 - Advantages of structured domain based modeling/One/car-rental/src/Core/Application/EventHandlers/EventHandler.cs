namespace Application.EventHandlers;
public class EventHandler : IEventHandler
{
    public EventHandler(IHandler handler)
    {
        handler.Add<CarGetByID, CarGetByIDEventHandler>();
        handler.Add<CarGet, CarGetEventHandler>();
        handler.Add<CreateCar, CreateCarEventHandler>();
        handler.Add<DeleteCar, DeleteCarEventHandler>();
        handler.Add<UpdateCar, UpdateCarEventHandler>();
        handler.Add<CreateCustomer, CreateCustomerEventHandler>();
        handler.Add<CustomerGetByID, CustomerGetByIDEventHandler>();
        handler.Add<CustomerGet, CustomerGetEventHandler>();
        handler.Add<DeleteCustomer, DeleteCustomerEventHandler>();
        handler.Add<UpdateCustomer, UpdateCustomerEventHandler>();
        handler.Add<CreateRent, CreateRentEventHandler>();
        handler.Add<DeleteRent, DeleteRentEventHandler>();
        handler.Add<RentGetByID, RentGetByIDEventHandler>();
        handler.Add<RentGet, RentGetEventHandler>();
        handler.Add<UpdateRent, UpdateRentEventHandler>();
    }
}