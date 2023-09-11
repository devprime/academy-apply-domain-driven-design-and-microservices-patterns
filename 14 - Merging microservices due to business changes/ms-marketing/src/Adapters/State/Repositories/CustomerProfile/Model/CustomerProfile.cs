namespace DevPrime.State.Repositories.CustomerProfile.Model;
public class CustomerProfile
{
    [BsonId]
    [BsonElement("_id")]
    public ObjectId _Id { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Guid ID { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Guid CustomerID { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Photo { get; set; }
    public DateTime BirthDate { get; set; }
    public double Score { get; set; }
}