namespace Application.Interfaces.Adapters.State;
public interface ICarState
{
    ICarRepository Car { get; set; }
}