namespace DevPrime.Web.Models.CustomerProfile;
public class CustomerProfile
{
    public Guid CustomerID { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Photo { get; set; }
    public DateTime BirthDate { get; set; }
    public double Score { get; set; }
    public static Application.Services.CustomerProfile.Model.CustomerProfile ToApplication(DevPrime.Web.Models.CustomerProfile.CustomerProfile customerProfile)
    {
        if (customerProfile is null)
            return new Application.Services.CustomerProfile.Model.CustomerProfile();
        Application.Services.CustomerProfile.Model.CustomerProfile _customerProfile = new Application.Services.CustomerProfile.Model.CustomerProfile();
        _customerProfile.CustomerID = customerProfile.CustomerID;
        _customerProfile.Email = customerProfile.Email;
        _customerProfile.Name = customerProfile.Name;
        _customerProfile.Photo = customerProfile.Photo;
        _customerProfile.BirthDate = customerProfile.BirthDate;
        _customerProfile.Score = customerProfile.Score;
        return _customerProfile;
    }
    public static List<Application.Services.CustomerProfile.Model.CustomerProfile> ToApplication(IList<DevPrime.Web.Models.CustomerProfile.CustomerProfile> customerProfileList)
    {
        List<Application.Services.CustomerProfile.Model.CustomerProfile> _customerProfileList = new List<Application.Services.CustomerProfile.Model.CustomerProfile>();
        if (customerProfileList != null)
        {
            foreach (var customerProfile in customerProfileList)
            {
                Application.Services.CustomerProfile.Model.CustomerProfile _customerProfile = new Application.Services.CustomerProfile.Model.CustomerProfile();
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
    public virtual Application.Services.CustomerProfile.Model.CustomerProfile ToApplication()
    {
        var model = ToApplication(this);
        return model;
    }
}