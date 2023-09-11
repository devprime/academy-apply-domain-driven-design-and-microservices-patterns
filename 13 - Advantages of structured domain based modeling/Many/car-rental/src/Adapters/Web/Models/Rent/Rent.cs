namespace DevPrime.Web.Models.Rent;
public class Rent
{
    public string LicensePlate { get; set; }
    public string TaxID { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public static Application.Services.Rent.Model.Rent ToApplication(DevPrime.Web.Models.Rent.Rent rent)
    {
        if (rent is null)
            return new Application.Services.Rent.Model.Rent();
        Application.Services.Rent.Model.Rent _rent = new Application.Services.Rent.Model.Rent();
        _rent.LicensePlate = rent.LicensePlate;
        _rent.TaxID = rent.TaxID;
        _rent.Start = rent.Start;
        _rent.End = rent.End;
        return _rent;
    }
    public static List<Application.Services.Rent.Model.Rent> ToApplication(IList<DevPrime.Web.Models.Rent.Rent> rentList)
    {
        List<Application.Services.Rent.Model.Rent> _rentList = new List<Application.Services.Rent.Model.Rent>();
        if (rentList != null)
        {
            foreach (var rent in rentList)
            {
                Application.Services.Rent.Model.Rent _rent = new Application.Services.Rent.Model.Rent();
                _rent.LicensePlate = rent.LicensePlate;
                _rent.TaxID = rent.TaxID;
                _rent.Start = rent.Start;
                _rent.End = rent.End;
                _rentList.Add(_rent);
            }
        }
        return _rentList;
    }
    public virtual Application.Services.Rent.Model.Rent ToApplication()
    {
        var model = ToApplication(this);
        return model;
    }
}