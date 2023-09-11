namespace Application.Services.Order.Model;
public class OrderGetByIDEventDTO
{
    public Guid ID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
}