namespace Domain.Aggregates.Car.Events;
public class CarGet : DomainEvent
{
    public CarGet() : base()
    {
    }
    public int? Limit { get; set; }
    public int? Offset { get; set; }
    public string Ordering { get; set; }
    public string Filter { get; set; }
    public string Sort { get; set; }
}