namespace DevPrime.State.Repositories.Customer.Model;
public class Customer
{
    [BsonId]
    [BsonElement("_id")]
    public ObjectId _Id { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Guid ID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}