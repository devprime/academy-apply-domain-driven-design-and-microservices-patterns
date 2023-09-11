namespace Application.Services.Order.Model;
public class Item
{
    public Guid ID { get; set; }
    public Application.Services.Order.Model.Mesure Amount { get; set; }
    public double ParcialPrice { get; set; }
    public string Product { get; set; }
    public virtual IList<Item> ToItemList(IList<Domain.Aggregates.Order.Item> itemList)
    {
        var _itemList = ToApplication(itemList);
        return _itemList;
    }
    public virtual Item ToItem(Domain.Aggregates.Order.Item item)
    {
        var _item = ToApplication(item);
        return _item;
    }
    public virtual Domain.Aggregates.Order.Item ToDomain()
    {
        var _item = ToDomain(this);
        return _item;
    }
    public virtual Domain.Aggregates.Order.Item ToDomain(Guid id)
    {
        var _item = new Domain.Aggregates.Order.Item();
        _item.ID = id;
        return _item;
    }
    public Item()
    {
    }
    public Item(Guid id)
    {
        ID = id;
    }
    public static Application.Services.Order.Model.Item ToApplication(Domain.Aggregates.Order.Item item)
    {
        if (item is null)
            return new Application.Services.Order.Model.Item();
        Application.Services.Order.Model.Item _item = new Application.Services.Order.Model.Item();
        _item.ID = item.ID;
        _item.Amount = Application.Services.Order.Model.Mesure.ToApplication(item.Amount);
        _item.ParcialPrice = item.ParcialPrice;
        _item.Product = item.Product;
        return _item;
    }
    public static List<Application.Services.Order.Model.Item> ToApplication(IList<Domain.Aggregates.Order.Item> itemList)
    {
        List<Application.Services.Order.Model.Item> _itemList = new List<Application.Services.Order.Model.Item>();
        if (itemList != null)
        {
            foreach (var item in itemList)
            {
                Application.Services.Order.Model.Item _item = new Application.Services.Order.Model.Item();
                _item.ID = item.ID;
                _item.Amount = Application.Services.Order.Model.Mesure.ToApplication(item.Amount);
                _item.ParcialPrice = item.ParcialPrice;
                _item.Product = item.Product;
                _itemList.Add(_item);
            }
        }
        return _itemList;
    }
    public static Domain.Aggregates.Order.Item ToDomain(Application.Services.Order.Model.Item item)
    {
        if (item is null)
            return new Domain.Aggregates.Order.Item();
        Domain.Aggregates.Order.Item _item = new Domain.Aggregates.Order.Item(item.ID, Application.Services.Order.Model.Mesure.ToDomain(item.Amount), item.ParcialPrice, item.Product);
        return _item;
    }
    public static List<Domain.Aggregates.Order.Item> ToDomain(IList<Application.Services.Order.Model.Item> itemList)
    {
        List<Domain.Aggregates.Order.Item> _itemList = new List<Domain.Aggregates.Order.Item>();
        if (itemList != null)
        {
            foreach (var item in itemList)
            {
                Domain.Aggregates.Order.Item _item = new Domain.Aggregates.Order.Item(item.ID, Application.Services.Order.Model.Mesure.ToDomain(item.Amount), item.ParcialPrice, item.Product);
                _itemList.Add(_item);
            }
        }
        return _itemList;
    }
}