namespace Application.Services.PromoCode.Model;
public class DeletePromoCodeEventDTO
{
    public Guid ID { get; set; }
    public string Code { get; set; }
    public double PercentageDiscount { get; set; }
    public bool Active { get; set; }
    public DateTime ValidUntil { get; set; }
}