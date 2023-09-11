namespace Application.Services.CustomerProfile.Model;
public class CustomerProfile
{
    internal int? Limit { get; set; }
    internal int? Offset { get; set; }
    internal string Ordering { get; set; }
    internal string Filter { get; set; }
    internal string Sort { get; set; }
    public CustomerProfile(int? limit, int? offset, string ordering, string sort, string filter)
    {
        Limit = limit;
        Offset = offset;
        Ordering = ordering;
        Filter = filter;
        Sort = sort;
    }
    public Guid ID { get; set; }
    public Guid CustomerID { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Photo { get; set; }
    public DateTime BirthDate { get; set; }
    public double Score { get; set; }
    public virtual PagingResult<IList<CustomerProfile>> ToCustomerProfileList(IList<Domain.Aggregates.CustomerProfile.CustomerProfile> customerProfileList, long? total, int? offSet, int? limit)
    {
        var _customerProfileList = ToApplication(customerProfileList);
        return new PagingResult<IList<CustomerProfile>>(_customerProfileList, total, offSet, limit);
    }
    public virtual CustomerProfile ToCustomerProfile(Domain.Aggregates.CustomerProfile.CustomerProfile customerProfile)
    {
        var _customerProfile = ToApplication(customerProfile);
        return _customerProfile;
    }
    public virtual Domain.Aggregates.CustomerProfile.CustomerProfile ToDomain()
    {
        var _customerProfile = ToDomain(this);
        return _customerProfile;
    }
    public virtual Domain.Aggregates.CustomerProfile.CustomerProfile ToDomain(Guid id)
    {
        var _customerProfile = new Domain.Aggregates.CustomerProfile.CustomerProfile();
        _customerProfile.ID = id;
        return _customerProfile;
    }
    public CustomerProfile()
    {
    }
    public CustomerProfile(Guid id)
    {
        ID = id;
    }
    public static Application.Services.CustomerProfile.Model.CustomerProfile ToApplication(Domain.Aggregates.CustomerProfile.CustomerProfile customerProfile)
    {
        if (customerProfile is null)
            return new Application.Services.CustomerProfile.Model.CustomerProfile();
        Application.Services.CustomerProfile.Model.CustomerProfile _customerProfile = new Application.Services.CustomerProfile.Model.CustomerProfile();
        _customerProfile.ID = customerProfile.ID;
        _customerProfile.CustomerID = customerProfile.CustomerID;
        _customerProfile.Email = customerProfile.Email;
        _customerProfile.Name = customerProfile.Name;
        _customerProfile.Photo = customerProfile.Photo;
        _customerProfile.BirthDate = customerProfile.BirthDate;
        _customerProfile.Score = customerProfile.Score;
        return _customerProfile;
    }
    public static List<Application.Services.CustomerProfile.Model.CustomerProfile> ToApplication(IList<Domain.Aggregates.CustomerProfile.CustomerProfile> customerProfileList)
    {
        List<Application.Services.CustomerProfile.Model.CustomerProfile> _customerProfileList = new List<Application.Services.CustomerProfile.Model.CustomerProfile>();
        if (customerProfileList != null)
        {
            foreach (var customerProfile in customerProfileList)
            {
                Application.Services.CustomerProfile.Model.CustomerProfile _customerProfile = new Application.Services.CustomerProfile.Model.CustomerProfile();
                _customerProfile.ID = customerProfile.ID;
                _customerProfile.CustomerID = customerProfile.CustomerID;
                _customerProfile.Email = customerProfile.Email;
                _customerProfile.Name = customerProfile.Name;
                _customerProfile.Photo = customerProfile.Photo;
                _customerProfile.BirthDate = customerProfile.BirthDate;
                _customerProfile.Score = customerProfile.Score;
                _customerProfileList.Add(_customerProfile);
            }
        }
        return _customerProfileList;
    }
    public static Domain.Aggregates.CustomerProfile.CustomerProfile ToDomain(Application.Services.CustomerProfile.Model.CustomerProfile customerProfile)
    {
        if (customerProfile is null)
            return new Domain.Aggregates.CustomerProfile.CustomerProfile();
        Domain.Aggregates.CustomerProfile.CustomerProfile _customerProfile = new Domain.Aggregates.CustomerProfile.CustomerProfile(customerProfile.ID, customerProfile.CustomerID, customerProfile.Email, customerProfile.Name, customerProfile.Photo, customerProfile.BirthDate, customerProfile.Score);
        return _customerProfile;
    }
    public static List<Domain.Aggregates.CustomerProfile.CustomerProfile> ToDomain(IList<Application.Services.CustomerProfile.Model.CustomerProfile> customerProfileList)
    {
        List<Domain.Aggregates.CustomerProfile.CustomerProfile> _customerProfileList = new List<Domain.Aggregates.CustomerProfile.CustomerProfile>();
        if (customerProfileList != null)
        {
            foreach (var customerProfile in customerProfileList)
            {
                Domain.Aggregates.CustomerProfile.CustomerProfile _customerProfile = new Domain.Aggregates.CustomerProfile.CustomerProfile(customerProfile.ID, customerProfile.CustomerID, customerProfile.Email, customerProfile.Name, customerProfile.Photo, customerProfile.BirthDate, customerProfile.Score);
                _customerProfileList.Add(_customerProfile);
            }
        }
        return _customerProfileList;
    }
}