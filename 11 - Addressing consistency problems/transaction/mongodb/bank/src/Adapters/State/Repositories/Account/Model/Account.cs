namespace DevPrime.State.Repositories.Account.Model;
public class Account
{
    [BsonId]
    [BsonElement("_id")]

    public ObjectId Id { get; set; }
    [BsonRepresentation(BsonType.String)]

    public Guid AccountID { get; set; }
    public string Number { get; set; }
    public double Balance { get; set; }
}