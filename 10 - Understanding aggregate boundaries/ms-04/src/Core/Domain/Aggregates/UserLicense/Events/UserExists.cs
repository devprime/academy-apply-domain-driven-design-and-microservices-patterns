namespace Domain.Aggregates.UserLicense.Events;
public class UserExists : DomainEvent
{
    public UserExists() : base()
    {
    }
    public Guid UserID { get; set; }
}