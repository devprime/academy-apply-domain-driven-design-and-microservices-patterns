namespace Domain.Aggregates.UserLicense.Events;
public class UserLicenseGet : DomainEvent
{
    public UserLicenseGet() : base()
    {
    }
    public int? Limit { get; set; }
    public int? Offset { get; set; }
    public string Ordering { get; set; }
    public string Filter { get; set; }
    public string Sort { get; set; }
}