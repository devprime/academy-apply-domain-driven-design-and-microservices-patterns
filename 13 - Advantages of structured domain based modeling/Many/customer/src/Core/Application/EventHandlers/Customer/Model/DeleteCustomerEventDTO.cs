namespace Application.Services.Customer.Model;
public class DeleteCustomerEventDTO
{
    public Guid ID { get; set; }
    public string Name { get; set; }
    public string TaxID { get; set; }
    public string Email { get; set; }
}