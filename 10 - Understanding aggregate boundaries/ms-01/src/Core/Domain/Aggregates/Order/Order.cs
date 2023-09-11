
namespace Domain.Aggregates.Order;
public class Order : AggRoot
{
    public string CustomerName { get; private set; }
    public List<Item> Items { get; private set; }
    public double Total { get; private set; }
    public double GetTotal()
    {
        double total = 0;
        if (Items != null)
        {
            foreach (var i in Items)
            {

                total += i.GetSubTotal();
            }
        }
        return total;
    }

    public virtual void Add()
    {
       //your add code goes here
    }
}