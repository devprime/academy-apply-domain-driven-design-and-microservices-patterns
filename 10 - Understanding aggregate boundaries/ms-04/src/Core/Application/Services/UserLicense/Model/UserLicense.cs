namespace Application.Services.UserLicense.Model;
public class UserLicense
{
    internal int? Limit { get; set; }
    internal int? Offset { get; set; }
    internal string Ordering { get; set; }
    internal string Filter { get; set; }
    internal string Sort { get; set; }
    public UserLicense(int? limit, int? offset, string ordering, string sort, string filter)
    {
        Limit = limit;
        Offset = offset;
        Ordering = ordering;
        Filter = filter;
        Sort = sort;
    }
    public Guid ID { get; set; }
    public Guid UserID { get; set; }
    public Guid LicenseID { get; set; }
    public virtual PagingResult<IList<UserLicense>> ToUserLicenseList(IList<Domain.Aggregates.UserLicense.UserLicense> userLicenseList, long? total, int? offSet, int? limit)
    {
        var _userLicenseList = ToApplication(userLicenseList);
        return new PagingResult<IList<UserLicense>>(_userLicenseList, total, offSet, limit);
    }
    public virtual UserLicense ToUserLicense(Domain.Aggregates.UserLicense.UserLicense userLicense)
    {
        var _userLicense = ToApplication(userLicense);
        return _userLicense;
    }
    public virtual Domain.Aggregates.UserLicense.UserLicense ToDomain()
    {
        var _userLicense = ToDomain(this);
        return _userLicense;
    }
    public virtual Domain.Aggregates.UserLicense.UserLicense ToDomain(Guid id)
    {
        var _userLicense = new Domain.Aggregates.UserLicense.UserLicense();
        _userLicense.ID = id;
        return _userLicense;
    }
    public UserLicense()
    {
    }
    public UserLicense(Guid id)
    {
        ID = id;
    }
    public static Application.Services.UserLicense.Model.UserLicense ToApplication(Domain.Aggregates.UserLicense.UserLicense userLicense)
    {
        if (userLicense is null)
            return new Application.Services.UserLicense.Model.UserLicense();
        Application.Services.UserLicense.Model.UserLicense _userLicense = new Application.Services.UserLicense.Model.UserLicense();
        _userLicense.ID = userLicense.ID;
        _userLicense.UserID = userLicense.UserID;
        _userLicense.LicenseID = userLicense.LicenseID;
        return _userLicense;
    }
    public static List<Application.Services.UserLicense.Model.UserLicense> ToApplication(IList<Domain.Aggregates.UserLicense.UserLicense> userLicenseList)
    {
        List<Application.Services.UserLicense.Model.UserLicense> _userLicenseList = new List<Application.Services.UserLicense.Model.UserLicense>();
        if (userLicenseList != null)
        {
            foreach (var userLicense in userLicenseList)
            {
                Application.Services.UserLicense.Model.UserLicense _userLicense = new Application.Services.UserLicense.Model.UserLicense();
                _userLicense.ID = userLicense.ID;
                _userLicense.UserID = userLicense.UserID;
                _userLicense.LicenseID = userLicense.LicenseID;
                _userLicenseList.Add(_userLicense);
            }
        }
        return _userLicenseList;
    }
    public static Domain.Aggregates.UserLicense.UserLicense ToDomain(Application.Services.UserLicense.Model.UserLicense userLicense)
    {
        if (userLicense is null)
            return new Domain.Aggregates.UserLicense.UserLicense();
        Domain.Aggregates.UserLicense.UserLicense _userLicense = new Domain.Aggregates.UserLicense.UserLicense(userLicense.ID, userLicense.UserID, userLicense.LicenseID);
        return _userLicense;
    }
    public static List<Domain.Aggregates.UserLicense.UserLicense> ToDomain(IList<Application.Services.UserLicense.Model.UserLicense> userLicenseList)
    {
        List<Domain.Aggregates.UserLicense.UserLicense> _userLicenseList = new List<Domain.Aggregates.UserLicense.UserLicense>();
        if (userLicenseList != null)
        {
            foreach (var userLicense in userLicenseList)
            {
                Domain.Aggregates.UserLicense.UserLicense _userLicense = new Domain.Aggregates.UserLicense.UserLicense(userLicense.ID, userLicense.UserID, userLicense.LicenseID);
                _userLicenseList.Add(_userLicense);
            }
        }
        return _userLicenseList;
    }
}