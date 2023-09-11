namespace DevPrime.State.Repositories.User.Model;
public class User
{
    [BsonId]
    [BsonElement("_id")]
    public ObjectId _Id { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Guid ID { get; set; }
    public string Name { get; set; }
}