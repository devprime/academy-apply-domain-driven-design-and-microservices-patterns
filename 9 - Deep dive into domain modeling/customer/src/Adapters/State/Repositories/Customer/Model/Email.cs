namespace DevPrime.State.Repositories.Customer.Model;
public class Email
{
    [BsonId]
    [BsonElement("_id")]
    public ObjectId _Id { get; set; }
    public string Value { get; set; }
}