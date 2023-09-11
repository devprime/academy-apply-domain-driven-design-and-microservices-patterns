namespace Domain.Aggregates.User.Events;
public class UserGet : DomainEvent
{
    public UserGet() : base()
    {
    }
    public int? Limit { get; set; }
    public int? Offset { get; set; }
    public string Ordering { get; set; }
    public string Filter { get; set; }
    public string Sort { get; set; }
}