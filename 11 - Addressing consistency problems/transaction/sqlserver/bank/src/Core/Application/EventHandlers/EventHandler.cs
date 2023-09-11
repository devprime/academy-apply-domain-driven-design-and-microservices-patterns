namespace Application.EventHandlers;
public class EventHandler : IEventHandler
{
    public EventHandler(IHandler handler)
    {
        handler.Add<CreateAccount, CreateAccountEventHandler>();
        handler.Add<DeleteAccount, DeleteAccountEventHandler>();
        handler.Add<GetAccount, GetAccountEventHandler>();
        handler.Add<UpdateAccount, UpdateAccountEventHandler>();
    }
}