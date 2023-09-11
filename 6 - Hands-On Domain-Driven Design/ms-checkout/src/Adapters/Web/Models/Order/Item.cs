namespace DevPrime.Web.Models.Order;
public class Item
{
    public DevPrime.Web.Models.Order.Mesure Amount { get; set; }
    public double ParcialPrice { get; set; }
    public string Product { get; set; }
    public static Application.Services.Order.Model.Item ToApplication(DevPrime.Web.Models.Order.Item item)
    {
        if (item is null)
            return new Application.Services.Order.Model.Item();
        Application.Services.Order.Model.Item _item = new Application.Services.Order.Model.Item();
        _item.Amount = DevPrime.Web.Models.Order.Mesure.ToApplication(item.Amount);
        _item.ParcialPrice = item.ParcialPrice;
        _item.Product = item.Product;
        return _item;
    }
    public static List<Application.Services.Order.Model.Item> ToApplication(IList<DevPrime.Web.Models.Order.Item> itemList)
    {
        List<Application.Services.Order.Model.Item> _itemList = new List<Application.Services.Order.Model.Item>();
        if (itemList != null)
        {
            foreach (var item in itemList)
            {
                Application.Services.Order.Model.Item _item = new Application.Services.Order.Model.Item();
                _item.Amount = DevPrime.Web.Models.Order.Mesure.ToApplication(item.Amount);
                _item.ParcialPrice = item.ParcialPrice;
                _item.Product = item.Product;
                _itemList.Add(_item);
            }
        }
        return _itemList;
    }
    public virtual Application.Services.Order.Model.Item ToApplication()
    {
        var model = ToApplication(this);
        return model;
    }
}