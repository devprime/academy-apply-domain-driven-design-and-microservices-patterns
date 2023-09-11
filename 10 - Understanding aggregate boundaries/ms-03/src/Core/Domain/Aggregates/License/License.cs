namespace Domain.Aggregates.License;
public class License : AggRoot
{
    public string Description { get; private set;}
    public LicenseType Type { get; private set;}
    public List<Guid> Users { get; private set;}
}