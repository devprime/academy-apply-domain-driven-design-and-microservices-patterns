namespace Domain.Aggregates.Order;
public class Item : Entity
{
    public int Quantity { get; private set; }
    public string SKU { get; private set; }
    public double Price { get; private set; }
    public Item(Guid id, int quantity, string sKU, double price)
    {
        ID = (id == Guid.Empty ? Guid.NewGuid() : id);
        Quantity = quantity;
        SKU = sKU;
        Price = price;
    }
    public Item()
    {
    }
}