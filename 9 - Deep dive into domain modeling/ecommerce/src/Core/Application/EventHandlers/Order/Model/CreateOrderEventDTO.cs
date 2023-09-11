namespace Application.Services.Order.Model;
public class CreateOrderEventDTO
{
    public Guid ID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
}