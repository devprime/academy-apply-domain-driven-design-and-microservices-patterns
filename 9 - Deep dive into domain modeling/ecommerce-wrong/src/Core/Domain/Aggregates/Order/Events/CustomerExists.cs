namespace Domain.Aggregates.Order.Events;
public class CustomerExists : DomainEvent
{
    public string Email { get; set;}
    public CustomerExists(string email) : base()
    {
        Email = email;
    }
}