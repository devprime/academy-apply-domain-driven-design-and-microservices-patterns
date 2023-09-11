namespace DevPrime.State.Repositories.UserLicense.Model;
public class UserLicense
{
    [BsonId]
    [BsonElement("_id")]
    public ObjectId _Id { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Guid ID { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Guid UserID { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Guid LicenseID { get; set; }
}