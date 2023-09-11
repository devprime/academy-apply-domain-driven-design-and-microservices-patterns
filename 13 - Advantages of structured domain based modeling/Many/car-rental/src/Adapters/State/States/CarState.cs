namespace DevPrime.State.States;
public class CarState : ICarState
{
    public ICarRepository Car { get; set; }
    public CarState(ICarRepository car)
    {
        Car = car;
    }
}