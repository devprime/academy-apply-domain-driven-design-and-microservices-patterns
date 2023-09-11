namespace Application.EventHandlers.UserLicense;
public class UserExistsEventHandler : EventHandler<UserExists, IUserState>
{
    public UserExistsEventHandler(IUserState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(UserExists userExists)
    {
        var id = userExists.UserID;
        var result = Dp.State.User.Exists(id);
        return result;
    }
}