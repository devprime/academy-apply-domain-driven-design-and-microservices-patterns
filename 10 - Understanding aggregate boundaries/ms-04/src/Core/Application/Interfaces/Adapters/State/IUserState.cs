namespace Application.Interfaces.Adapters.State;
public interface IUserState
{
    IUserRepository User { get; set; }
}