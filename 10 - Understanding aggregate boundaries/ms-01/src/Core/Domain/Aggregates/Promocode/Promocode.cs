
namespace Domain.Aggregates.Promocode;
public class Promocode : AggRoot
{
    public string Code { get; private set;}
    public double Percentage { get; private set;}
    public bool IsActive { get; private set; }

    public void Disable()
    {
        IsActive = false;
    }
}