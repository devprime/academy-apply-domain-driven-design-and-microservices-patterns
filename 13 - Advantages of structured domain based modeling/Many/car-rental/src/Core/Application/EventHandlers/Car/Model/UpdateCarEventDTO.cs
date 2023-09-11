namespace Application.Services.Car.Model;
public class UpdateCarEventDTO
{
    public Guid ID { get; set; }
    public string Model { get; set; }
    public string LicensePlate { get; set; }
}