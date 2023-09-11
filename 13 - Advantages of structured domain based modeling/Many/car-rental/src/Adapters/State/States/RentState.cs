namespace DevPrime.State.States;
public class RentState : IRentState
{
    public IRentRepository Rent { get; set; }
    public RentState(IRentRepository rent)
    {
        Rent = rent;
    }
}