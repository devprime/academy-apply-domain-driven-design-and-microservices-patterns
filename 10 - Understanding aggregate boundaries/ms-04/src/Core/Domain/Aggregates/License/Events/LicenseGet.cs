namespace Domain.Aggregates.License.Events;
public class LicenseGet : DomainEvent
{
    public LicenseGet() : base()
    {
    }
    public int? Limit { get; set; }
    public int? Offset { get; set; }
    public string Ordering { get; set; }
    public string Filter { get; set; }
    public string Sort { get; set; }
}