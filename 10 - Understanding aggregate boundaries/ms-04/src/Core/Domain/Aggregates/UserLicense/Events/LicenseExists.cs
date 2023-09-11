namespace Domain.Aggregates.UserLicense.Events;
public class LicenseExists : DomainEvent
{
    public LicenseExists() : base()
    {
    }
    public Guid LicenseID { get; set; }
}