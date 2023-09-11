namespace Application.Services.Car.Model;
public class CreateCarEventDTO
{
    public Guid ID { get; set; }
    public string Model { get; set; }
    public string LicensePlate { get; set; }
}