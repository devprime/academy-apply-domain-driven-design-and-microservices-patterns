namespace Application.Services.Order.Model;
public class DeleteOrderEventDTO
{
    public Guid ID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
}