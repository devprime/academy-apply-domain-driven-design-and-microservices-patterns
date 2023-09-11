namespace Application.EventHandlers.User;
public class UpdateUserEventHandler : EventHandler<UpdateUser, IUserState>
{
    public UpdateUserEventHandler(IUserState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(UpdateUser updateUser)
    {
        var user = updateUser.Get<Domain.Aggregates.User.User>();
        return Dp.State.User.Update(user);
    }
}