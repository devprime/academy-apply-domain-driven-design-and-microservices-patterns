namespace Domain.DomainEvents;
public class GetCustomerByEmail : DomainEvent
{
    public string Email { get; set;}
    public GetCustomerByEmail(string email) : base()
    {
        Email = email;
    }
}
