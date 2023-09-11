namespace Application.EventHandlers.User;
public class CreateUserEventHandler : EventHandler<CreateUser, IUserState>
{
    public CreateUserEventHandler(IUserState state, IDp dp) : base(state, dp)
    {
    }
    public override dynamic Handle(CreateUser createUser)
    {
        var user = createUser.Get<Domain.Aggregates.User.User>();
        return Dp.State.User.Add(user);
    }
}