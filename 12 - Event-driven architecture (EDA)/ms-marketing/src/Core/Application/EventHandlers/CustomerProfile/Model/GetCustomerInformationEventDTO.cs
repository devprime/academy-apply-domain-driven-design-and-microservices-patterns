namespace Application.Services.CustomerProfile.Model;
public class GetCustomerInformationEventDTO : ServicesResult
{
    public Guid ID { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
}
