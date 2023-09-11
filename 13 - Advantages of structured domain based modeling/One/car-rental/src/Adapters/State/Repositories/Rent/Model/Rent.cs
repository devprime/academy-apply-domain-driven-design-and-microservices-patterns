namespace DevPrime.State.Repositories.Rent.Model;
public class Rent
{
    [BsonId]
    [BsonElement("_id")]
    public ObjectId _Id { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Guid ID { get; set; }
    public string LicensePlate { get; set; }
    public string TaxID { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}