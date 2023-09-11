namespace DevPrime.State.States;
public class UserState : IUserState
{
    public IUserRepository User { get; set; }
    public UserState(IUserRepository user)
    {
        User = user;
    }
}