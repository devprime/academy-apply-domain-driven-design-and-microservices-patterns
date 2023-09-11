namespace Application.Interfaces.Adapters.State;
public interface IAccountState
{
    IAccountRepository Account { get; set; }
}