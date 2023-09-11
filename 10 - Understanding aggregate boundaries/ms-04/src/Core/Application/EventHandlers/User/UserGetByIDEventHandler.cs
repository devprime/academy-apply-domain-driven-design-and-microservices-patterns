namespace Application.EventHandlers.User;
public class UserGetByIDEventHandler : EventHandler<UserGetByID, IUserState>
{
    public UserGetByIDEventHandler(IUserState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(UserGetByID userGetByID)
    {
        var user = userGetByID.Get<Domain.Aggregates.User.User>();
        return Dp.State.User.Get(user.ID);
    }
}