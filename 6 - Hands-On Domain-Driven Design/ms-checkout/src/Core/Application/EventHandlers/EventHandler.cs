using Domain.DomainEvents;
namespace Application.EventHandlers;
public class EventHandler : IEventHandler
{
    public EventHandler(IHandler handler)
    {
        handler.Add<GetPromoCodeByCode, GetPromoCodeByCodeEventHandler>();
        handler.Add<CreateOrder, CreateOrderEventHandler>();
        handler.Add<DeleteOrder, DeleteOrderEventHandler>();
        handler.Add<OrderCreated, OrderCreatedEventHandler>();
        handler.Add<OrderDeleted, OrderDeletedEventHandler>();
        handler.Add<OrderGetByID, OrderGetByIDEventHandler>();
        handler.Add<OrderGet, OrderGetEventHandler>();
        handler.Add<OrderUpdated, OrderUpdatedEventHandler>();
        handler.Add<UpdateOrder, UpdateOrderEventHandler>();
        handler.Add<CreatePromoCode, CreatePromoCodeEventHandler>();
        handler.Add<DeletePromoCode, DeletePromoCodeEventHandler>();
        handler.Add<PromoCodeCreated, PromoCodeCreatedEventHandler>();
        handler.Add<PromoCodeDeleted, PromoCodeDeletedEventHandler>();
        handler.Add<PromoCodeGetByID, PromoCodeGetByIDEventHandler>();
        handler.Add<PromoCodeGet, PromoCodeGetEventHandler>();
        handler.Add<PromoCodeUpdated, PromoCodeUpdatedEventHandler>();
        handler.Add<UpdatePromoCode, UpdatePromoCodeEventHandler>();
    }
}