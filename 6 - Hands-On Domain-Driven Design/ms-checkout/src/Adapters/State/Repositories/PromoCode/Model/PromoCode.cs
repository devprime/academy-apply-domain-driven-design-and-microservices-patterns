namespace DevPrime.State.Repositories.PromoCode.Model;
public class PromoCode
{
    [BsonId]
    [BsonElement("_id")]
    public ObjectId _Id { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Guid ID { get; set; }
    public string Code { get; set; }
    public double PercentageDiscount { get; set; }
    public bool Active { get; set; }
    public DateTime ValidUntil { get; set; }
}