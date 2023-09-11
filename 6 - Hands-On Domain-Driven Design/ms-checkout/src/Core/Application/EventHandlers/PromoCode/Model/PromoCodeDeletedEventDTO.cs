namespace Application.Services.PromoCode.Model;
public class PromoCodeDeletedEventDTO
{
    public Guid ID { get; set; }
    public string Code { get; set; }
    public double PercentageDiscount { get; set; }
    public bool Active { get; set; }
    public DateTime ValidUntil { get; set; }
}