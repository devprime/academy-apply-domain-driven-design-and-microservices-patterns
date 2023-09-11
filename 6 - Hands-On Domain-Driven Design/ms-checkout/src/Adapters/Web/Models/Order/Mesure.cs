namespace DevPrime.Web.Models.Order;
public class Mesure
{
    public string UnitOfMesure { get; set; }
    public double Quantity { get; set; }
    public static Application.Services.Order.Model.Mesure ToApplication(DevPrime.Web.Models.Order.Mesure mesure)
    {
        if (mesure is null)
            return new Application.Services.Order.Model.Mesure();
        Application.Services.Order.Model.Mesure _mesure = new Application.Services.Order.Model.Mesure();
        _mesure.UnitOfMesure = mesure.UnitOfMesure?.ToString();
        _mesure.Quantity = mesure.Quantity;
        return _mesure;
    }
    public static List<Application.Services.Order.Model.Mesure> ToApplication(IList<DevPrime.Web.Models.Order.Mesure> mesureList)
    {
        List<Application.Services.Order.Model.Mesure> _mesureList = new List<Application.Services.Order.Model.Mesure>();
        if (mesureList != null)
        {
            foreach (var mesure in mesureList)
            {
                Application.Services.Order.Model.Mesure _mesure = new Application.Services.Order.Model.Mesure();
                _mesure.UnitOfMesure = mesure.UnitOfMesure?.ToString();
                _mesure.Quantity = mesure.Quantity;
                _mesureList.Add(_mesure);
            }
        }
        return _mesureList;
    }
    public virtual Application.Services.Order.Model.Mesure ToApplication()
    {
        var model = ToApplication(this);
        return model;
    }
}