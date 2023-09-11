namespace Application.Services.Order.Model;
public class Mesure
{
    public string UnitOfMesure { get; set; }
    public double Quantity { get; set; }
    public virtual IList<Mesure> ToMesureList(IList<Domain.Aggregates.Order.Mesure> mesureList)
    {
        var _mesureList = ToApplication(mesureList);
        return _mesureList;
    }
    public virtual Mesure ToMesure(Domain.Aggregates.Order.Mesure mesure)
    {
        var _mesure = ToApplication(mesure);
        return _mesure;
    }
    public virtual Domain.Aggregates.Order.Mesure ToDomain()
    {
        var _mesure = ToDomain(this);
        return _mesure;
    }
    public Mesure()
    {
    }
    public static Application.Services.Order.Model.Mesure ToApplication(Domain.Aggregates.Order.Mesure mesure)
    {
        if (mesure is null)
            return new Application.Services.Order.Model.Mesure();
        Application.Services.Order.Model.Mesure _mesure = new Application.Services.Order.Model.Mesure();
        _mesure.UnitOfMesure = mesure.UnitOfMesure.ToString();
        _mesure.Quantity = mesure.Quantity;
        return _mesure;
    }
    public static List<Application.Services.Order.Model.Mesure> ToApplication(IList<Domain.Aggregates.Order.Mesure> mesureList)
    {
        List<Application.Services.Order.Model.Mesure> _mesureList = new List<Application.Services.Order.Model.Mesure>();
        if (mesureList != null)
        {
            foreach (var mesure in mesureList)
            {
                Application.Services.Order.Model.Mesure _mesure = new Application.Services.Order.Model.Mesure();
                _mesure.UnitOfMesure = mesure.UnitOfMesure.ToString();
                _mesure.Quantity = mesure.Quantity;
                _mesureList.Add(_mesure);
            }
        }
        return _mesureList;
    }
    public static Domain.Aggregates.Order.Mesure ToDomain(Application.Services.Order.Model.Mesure mesure)
    {
        if (mesure is null)
            return new Domain.Aggregates.Order.Mesure();
        Domain.Aggregates.Order.Mesure _mesure = new Domain.Aggregates.Order.Mesure(mesure.UnitOfMesure, mesure.Quantity);
        return _mesure;
    }
    public static List<Domain.Aggregates.Order.Mesure> ToDomain(IList<Application.Services.Order.Model.Mesure> mesureList)
    {
        List<Domain.Aggregates.Order.Mesure> _mesureList = new List<Domain.Aggregates.Order.Mesure>();
        if (mesureList != null)
        {
            foreach (var mesure in mesureList)
            {
                Domain.Aggregates.Order.Mesure _mesure = new Domain.Aggregates.Order.Mesure(mesure.UnitOfMesure, mesure.Quantity);
                _mesureList.Add(_mesure);
            }
        }
        return _mesureList;
    }
}