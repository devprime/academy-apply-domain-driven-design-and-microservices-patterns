namespace Domain.Aggregates.Order;
public class Item : Entity
{
    public Mesure Amount { get; private set; }
    public double ParcialPrice { get; private set; }
    public string Product { get; private set; }
    public Item(Guid id, Domain.Aggregates.Order.Mesure amount, double parcialPrice, string product)
    {
        ID = (id == Guid.Empty ? Guid.NewGuid() : id);
        Amount = amount;
        ParcialPrice = parcialPrice;
        Product = product;
    }
    public Item()
    {
    }
    public double SubTotal()
    {
        var result = Dp.Pipeline(ExecuteResult: () =>
        {
            return ParcialPrice * Amount.Quantity;
        });
        return result;
    }

}