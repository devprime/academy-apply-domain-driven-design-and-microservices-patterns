namespace DevPrime.State.Repositories.Order.Model;
public class Item
{
    [BsonId]
    [BsonElement("_id")]
    public ObjectId _Id { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Guid ID { get; set; }
    public DevPrime.State.Repositories.Order.Model.Mesure Amount { get; set; }
    public double ParcialPrice { get; set; }
    public string Product { get; set; }
}