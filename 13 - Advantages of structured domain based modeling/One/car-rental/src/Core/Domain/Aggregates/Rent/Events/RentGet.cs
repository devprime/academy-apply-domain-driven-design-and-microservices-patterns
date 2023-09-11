namespace Domain.Aggregates.Rent.Events;
public class RentGet : DomainEvent
{
    public RentGet() : base()
    {
    }
    public int? Limit { get; set; }
    public int? Offset { get; set; }
    public string Ordering { get; set; }
    public string Filter { get; set; }
    public string Sort { get; set; }
}