namespace Application.Services.Rent.Model;
public class Rent
{
    internal int? Limit { get; set; }
    internal int? Offset { get; set; }
    internal string Ordering { get; set; }
    internal string Filter { get; set; }
    internal string Sort { get; set; }
    public Rent(int? limit, int? offset, string ordering, string sort, string filter)
    {
        Limit = limit;
        Offset = offset;
        Ordering = ordering;
        Filter = filter;
        Sort = sort;
    }
    public Guid ID { get; set; }
    public string LicensePlate { get; set; }
    public string TaxID { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public virtual PagingResult<IList<Rent>> ToRentList(IList<Domain.Aggregates.Rent.Rent> rentList, long? total, int? offSet, int? limit)
    {
        var _rentList = ToApplication(rentList);
        return new PagingResult<IList<Rent>>(_rentList, total, offSet, limit);
    }
    public virtual Rent ToRent(Domain.Aggregates.Rent.Rent rent)
    {
        var _rent = ToApplication(rent);
        return _rent;
    }
    public virtual Domain.Aggregates.Rent.Rent ToDomain()
    {
        var _rent = ToDomain(this);
        return _rent;
    }
    public virtual Domain.Aggregates.Rent.Rent ToDomain(Guid id)
    {
        var _rent = new Domain.Aggregates.Rent.Rent();
        _rent.ID = id;
        return _rent;
    }
    public Rent()
    {
    }
    public Rent(Guid id)
    {
        ID = id;
    }
    public static Application.Services.Rent.Model.Rent ToApplication(Domain.Aggregates.Rent.Rent rent)
    {
        if (rent is null)
            return new Application.Services.Rent.Model.Rent();
        Application.Services.Rent.Model.Rent _rent = new Application.Services.Rent.Model.Rent();
        _rent.ID = rent.ID;
        _rent.LicensePlate = rent.LicensePlate;
        _rent.TaxID = rent.TaxID;
        _rent.Start = rent.Start;
        _rent.End = rent.End;
        return _rent;
    }
    public static List<Application.Services.Rent.Model.Rent> ToApplication(IList<Domain.Aggregates.Rent.Rent> rentList)
    {
        List<Application.Services.Rent.Model.Rent> _rentList = new List<Application.Services.Rent.Model.Rent>();
        if (rentList != null)
        {
            foreach (var rent in rentList)
            {
                Application.Services.Rent.Model.Rent _rent = new Application.Services.Rent.Model.Rent();
                _rent.ID = rent.ID;
                _rent.LicensePlate = rent.LicensePlate;
                _rent.TaxID = rent.TaxID;
                _rent.Start = rent.Start;
                _rent.End = rent.End;
                _rentList.Add(_rent);
            }
        }
        return _rentList;
    }
    public static Domain.Aggregates.Rent.Rent ToDomain(Application.Services.Rent.Model.Rent rent)
    {
        if (rent is null)
            return new Domain.Aggregates.Rent.Rent();
        Domain.Aggregates.Rent.Rent _rent = new Domain.Aggregates.Rent.Rent(rent.ID, rent.LicensePlate, rent.TaxID, rent.Start, rent.End);
        return _rent;
    }
    public static List<Domain.Aggregates.Rent.Rent> ToDomain(IList<Application.Services.Rent.Model.Rent> rentList)
    {
        List<Domain.Aggregates.Rent.Rent> _rentList = new List<Domain.Aggregates.Rent.Rent>();
        if (rentList != null)
        {
            foreach (var rent in rentList)
            {
                Domain.Aggregates.Rent.Rent _rent = new Domain.Aggregates.Rent.Rent(rent.ID, rent.LicensePlate, rent.TaxID, rent.Start, rent.End);
                _rentList.Add(_rent);
            }
        }
        return _rentList;
    }
}