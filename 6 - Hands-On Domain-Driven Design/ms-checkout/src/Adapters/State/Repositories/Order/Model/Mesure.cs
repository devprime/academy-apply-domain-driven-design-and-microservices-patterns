namespace DevPrime.State.Repositories.Order.Model;
public class Mesure
{
    [BsonId]
    [BsonElement("_id")]
    public ObjectId _Id { get; set; }
    public string UnitOfMesure { get; set; }
    public double Quantity { get; set; }
}