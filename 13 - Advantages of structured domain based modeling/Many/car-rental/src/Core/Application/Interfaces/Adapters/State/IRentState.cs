namespace Application.Interfaces.Adapters.State;
public interface IRentState
{
    IRentRepository Rent { get; set; }
}