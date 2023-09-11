namespace Application.Services.License.Model;
public class License
{
    internal int? Limit { get; set; }
    internal int? Offset { get; set; }
    internal string Ordering { get; set; }
    internal string Filter { get; set; }
    internal string Sort { get; set; }
    public License(int? limit, int? offset, string ordering, string sort, string filter)
    {
        Limit = limit;
        Offset = offset;
        Ordering = ordering;
        Filter = filter;
        Sort = sort;
    }
    public Guid ID { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public virtual PagingResult<IList<License>> ToLicenseList(IList<Domain.Aggregates.License.License> licenseList, long? total, int? offSet, int? limit)
    {
        var _licenseList = ToApplication(licenseList);
        return new PagingResult<IList<License>>(_licenseList, total, offSet, limit);
    }
    public virtual License ToLicense(Domain.Aggregates.License.License license)
    {
        var _license = ToApplication(license);
        return _license;
    }
    public virtual Domain.Aggregates.License.License ToDomain()
    {
        var _license = ToDomain(this);
        return _license;
    }
    public virtual Domain.Aggregates.License.License ToDomain(Guid id)
    {
        var _license = new Domain.Aggregates.License.License();
        _license.ID = id;
        return _license;
    }
    public License()
    {
    }
    public License(Guid id)
    {
        ID = id;
    }
    public static Application.Services.License.Model.License ToApplication(Domain.Aggregates.License.License license)
    {
        if (license is null)
            return new Application.Services.License.Model.License();
        Application.Services.License.Model.License _license = new Application.Services.License.Model.License();
        _license.ID = license.ID;
        _license.Description = license.Description;
        _license.Type = license.Type.ToString();
        return _license;
    }
    public static List<Application.Services.License.Model.License> ToApplication(IList<Domain.Aggregates.License.License> licenseList)
    {
        List<Application.Services.License.Model.License> _licenseList = new List<Application.Services.License.Model.License>();
        if (licenseList != null)
        {
            foreach (var license in licenseList)
            {
                Application.Services.License.Model.License _license = new Application.Services.License.Model.License();
                _license.ID = license.ID;
                _license.Description = license.Description;
                _license.Type = license.Type.ToString();
                _licenseList.Add(_license);
            }
        }
        return _licenseList;
    }
    public static Domain.Aggregates.License.License ToDomain(Application.Services.License.Model.License license)
    {
        if (license is null)
            return new Domain.Aggregates.License.License();
        Domain.Aggregates.License.License _license = new Domain.Aggregates.License.License(license.ID, license.Description, license.Type);
        return _license;
    }
    public static List<Domain.Aggregates.License.License> ToDomain(IList<Application.Services.License.Model.License> licenseList)
    {
        List<Domain.Aggregates.License.License> _licenseList = new List<Domain.Aggregates.License.License>();
        if (licenseList != null)
        {
            foreach (var license in licenseList)
            {
                Domain.Aggregates.License.License _license = new Domain.Aggregates.License.License(license.ID, license.Description, license.Type);
                _licenseList.Add(_license);
            }
        }
        return _licenseList;
    }
}