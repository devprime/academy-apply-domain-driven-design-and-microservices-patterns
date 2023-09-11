namespace Domain.Aggregates.User;
public class User : AggRoot
{
    public string Name { get; private set;}
    
    public List<Guid> LicenseIDs { get; private set;}
}