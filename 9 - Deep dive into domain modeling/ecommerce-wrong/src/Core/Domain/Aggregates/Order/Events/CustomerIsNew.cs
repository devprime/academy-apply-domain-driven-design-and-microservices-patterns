namespace Domain.Aggregates.Order.Events;
public class CustomerIsNew : DomainEvent
{
    public string Email { get; set; }
    public string Name { get; set; }
    public CustomerIsNew(string email, string name) : base()
    {
        Email = email;
        Name = name;
    }
}