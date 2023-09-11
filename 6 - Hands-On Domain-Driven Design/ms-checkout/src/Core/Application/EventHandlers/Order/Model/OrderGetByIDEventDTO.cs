namespace Application.Services.Order.Model;
public class OrderGetByIDEventDTO
{
    public Guid ID { get; set; }
    public string CustomerTaxID { get; set; }
    public string CustomerName { get; set; }
    public string PromoCode { get; set; }
    public double TotalPrice { get; set; }
}