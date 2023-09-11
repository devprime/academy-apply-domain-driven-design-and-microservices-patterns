namespace DevPrime.Web.Models.UserLicense;
public class UserLicense
{
    public Guid UserID { get; set; }
    public Guid LicenseID { get; set; }
    public static Application.Services.UserLicense.Model.UserLicense ToApplication(DevPrime.Web.Models.UserLicense.UserLicense userLicense)
    {
        if (userLicense is null)
            return new Application.Services.UserLicense.Model.UserLicense();
        Application.Services.UserLicense.Model.UserLicense _userLicense = new Application.Services.UserLicense.Model.UserLicense();
        _userLicense.UserID = userLicense.UserID;
        _userLicense.LicenseID = userLicense.LicenseID;
        return _userLicense;
    }
    public static List<Application.Services.UserLicense.Model.UserLicense> ToApplication(IList<DevPrime.Web.Models.UserLicense.UserLicense> userLicenseList)
    {
        List<Application.Services.UserLicense.Model.UserLicense> _userLicenseList = new List<Application.Services.UserLicense.Model.UserLicense>();
        if (userLicenseList != null)
        {
            foreach (var userLicense in userLicenseList)
            {
                Application.Services.UserLicense.Model.UserLicense _userLicense = new Application.Services.UserLicense.Model.UserLicense();
                _userLicense.UserID = userLicense.UserID;
                _userLicense.LicenseID = userLicense.LicenseID;
                _userLicenseList.Add(_userLicense);
            }
        }
        return _userLicenseList;
    }
    public virtual Application.Services.UserLicense.Model.UserLicense ToApplication()
    {
        var model = ToApplication(this);
        return model;
    }
}