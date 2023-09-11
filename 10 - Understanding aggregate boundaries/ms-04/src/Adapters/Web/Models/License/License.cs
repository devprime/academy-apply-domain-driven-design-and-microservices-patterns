namespace DevPrime.Web.Models.License;
public class License
{
    public string Description { get; set; }
    public string Type { get; set; }
    public static Application.Services.License.Model.License ToApplication(DevPrime.Web.Models.License.License license)
    {
        if (license is null)
            return new Application.Services.License.Model.License();
        Application.Services.License.Model.License _license = new Application.Services.License.Model.License();
        _license.Description = license.Description;
        _license.Type = license.Type?.ToString();
        return _license;
    }
    public static List<Application.Services.License.Model.License> ToApplication(IList<DevPrime.Web.Models.License.License> licenseList)
    {
        List<Application.Services.License.Model.License> _licenseList = new List<Application.Services.License.Model.License>();
        if (licenseList != null)
        {
            foreach (var license in licenseList)
            {
                Application.Services.License.Model.License _license = new Application.Services.License.Model.License();
                _license.Description = license.Description;
                _license.Type = license.Type?.ToString();
                _licenseList.Add(_license);
            }
        }
        return _licenseList;
    }
    public virtual Application.Services.License.Model.License ToApplication()
    {
        var model = ToApplication(this);
        return model;
    }
}