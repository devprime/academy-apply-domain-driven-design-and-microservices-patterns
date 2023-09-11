namespace Domain.Aggregates.Order;
public class Item : Entity
{
    public string SKU { get; private set; }
    public double Price { get; private set; }
    public int Amount { get; private set; }
    public double GetSubTotal()
    {
        return Price * Amount;
    }
}