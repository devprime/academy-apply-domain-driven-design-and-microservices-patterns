namespace DevPrime.State.Repositories.Car.Model;
public class Car
{
    [BsonId]
    [BsonElement("_id")]
    public ObjectId _Id { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Guid ID { get; set; }
    public string Model { get; set; }
    public string LicensePlate { get; set; }
}