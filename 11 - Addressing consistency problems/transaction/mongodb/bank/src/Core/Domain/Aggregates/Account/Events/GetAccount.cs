namespace Domain.Aggregates.Account.Events;
public class GetAccount : DomainEvent
{
    public GetAccount() : base()
    {
    }

    public int? Limit { get; set; }
    public int? Offset { get; set; }
    public string Ordering { get; set; }
    public string Filter { get; set; }
    public string Sort { get; set; }
}