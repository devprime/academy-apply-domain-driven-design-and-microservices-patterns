namespace Application.EventHandlers.User;
public class DeleteUserEventHandler : EventHandler<DeleteUser, IUserState>
{
    public DeleteUserEventHandler(IUserState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(DeleteUser deleteUser)
    {
        var user = deleteUser.Get<Domain.Aggregates.User.User>();
        return Dp.State.User.Delete(user.ID);
    }
}