namespace Domain.Aggregates.CustomerProfile.Events;
public class CustomerProfileGet : DomainEvent
{
    public CustomerProfileGet() : base()
    {
    }
    public int? Limit { get; set; }
    public int? Offset { get; set; }
    public string Ordering { get; set; }
    public string Filter { get; set; }
    public string Sort { get; set; }
}