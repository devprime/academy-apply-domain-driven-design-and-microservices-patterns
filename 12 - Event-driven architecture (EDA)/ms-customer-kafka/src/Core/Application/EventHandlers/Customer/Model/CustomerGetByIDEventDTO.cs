namespace Application.Services.Customer.Model;
public class CustomerGetByIDEventDTO
{
    public Guid ID { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
}