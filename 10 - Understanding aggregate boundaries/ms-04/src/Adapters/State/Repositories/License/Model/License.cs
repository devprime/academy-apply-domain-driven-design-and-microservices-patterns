namespace DevPrime.State.Repositories.License.Model;
public class License
{
    [BsonId]
    [BsonElement("_id")]
    public ObjectId _Id { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Guid ID { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
}