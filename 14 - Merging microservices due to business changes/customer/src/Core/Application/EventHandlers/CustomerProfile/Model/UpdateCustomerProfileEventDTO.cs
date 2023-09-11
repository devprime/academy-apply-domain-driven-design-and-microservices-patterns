namespace Application.Services.CustomerProfile.Model;
public class UpdateCustomerProfileEventDTO
{
    public Guid ID { get; set; }
    public Guid CustomerID { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Photo { get; set; }
    public DateTime BirthDate { get; set; }
    public double Score { get; set; }
}