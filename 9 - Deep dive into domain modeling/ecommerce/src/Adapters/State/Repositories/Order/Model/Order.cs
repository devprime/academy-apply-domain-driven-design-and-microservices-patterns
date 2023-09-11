namespace DevPrime.State.Repositories.Order.Model;
public class Order
{
    [BsonId]
    [BsonElement("_id")]
    public ObjectId _Id { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Guid ID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public IList<DevPrime.State.Repositories.Order.Model.Item> Items { get; set; }
}