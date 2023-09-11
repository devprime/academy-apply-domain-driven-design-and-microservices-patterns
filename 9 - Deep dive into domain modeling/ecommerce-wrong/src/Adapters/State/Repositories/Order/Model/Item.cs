namespace DevPrime.State.Repositories.Order.Model;
public class Item
{
    [BsonId]
    [BsonElement("_id")]
    public ObjectId _Id { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Guid ID { get; set; }
    public int Quantity { get; set; }
    public string SKU { get; set; }
    public double Price { get; set; }
}