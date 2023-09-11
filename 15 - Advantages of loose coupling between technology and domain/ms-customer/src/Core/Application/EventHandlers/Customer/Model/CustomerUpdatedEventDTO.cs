namespace Application.Services.Customer.Model;
public class CustomerUpdatedEventDTO
{
    public Guid ID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}