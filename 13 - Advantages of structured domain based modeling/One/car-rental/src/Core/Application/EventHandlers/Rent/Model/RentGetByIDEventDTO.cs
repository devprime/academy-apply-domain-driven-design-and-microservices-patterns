namespace Application.Services.Rent.Model;
public class RentGetByIDEventDTO
{
    public Guid ID { get; set; }
    public string LicensePlate { get; set; }
    public string TaxID { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}