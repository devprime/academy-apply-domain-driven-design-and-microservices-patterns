namespace Domain.Aggregates.UserLicense;
public class UserLicense : AggRoot
{
    public Guid UserID { get; private set; }
    public Guid LicenseID { get; private set; }
    public UserLicense(Guid id, Guid userID, Guid licenseID)
    {
        ID = id;
        UserID = userID;
        LicenseID = licenseID;
    }
    public UserLicense()
    {
    }
    public virtual void Add()
    {
        Dp.Pipeline(Execute: () =>
        {
            ValidFields();
            ValidateIfUserExists();
            ValidateIfLicenseExists();
            ID = Guid.NewGuid();
            IsNew = true;
            var success = Dp.ProcessEvent<bool>(new CreateUserLicense());
        });
    }
    public virtual void Update()
    {
        Dp.Pipeline(Execute: () =>
        {
            if (ID.Equals(Guid.Empty))
                Dp.Notifications.Add("ID is required");
            ValidFields();
            ValidateIfUserExists();
            ValidateIfLicenseExists();
            var success = Dp.ProcessEvent<bool>(new UpdateUserLicense());
        });
    }
    public virtual void Delete()
    {
        Dp.Pipeline(Execute: () =>
        {
            if (ID != Guid.Empty)
            {
                var success = Dp.ProcessEvent<bool>(new DeleteUserLicense());
            }
        });
    }
    public virtual (List<UserLicense> Result, long Total) Get(int? limit, int? offset, string ordering, string sort, string filter)
    {
        return Dp.Pipeline(ExecuteResult: () =>
        {
            ValidateOrdering(limit, offset, ordering, sort);
            if (!string.IsNullOrWhiteSpace(filter))
            {
                bool filterIsValid = false;
                if (filter.Contains("="))
                {
                    if (filter.ToLower().StartsWith("id="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("userid="))
                        filterIsValid = true;
                    if (filter.ToLower().StartsWith("licenseid="))
                        filterIsValid = true;
                }
                if (!filterIsValid)
                    throw new PublicException($"Invalid filter '{filter}' is invalid try: 'ID', 'UserID', 'LicenseID',");
            }
            var source = Dp.ProcessEvent(new UserLicenseGet()
            {Limit = limit, Offset = offset, Ordering = ordering, Sort = sort, Filter = filter});
            return source;
        });
    }
    public virtual UserLicense GetByID()
    {
        var result = Dp.Pipeline(ExecuteResult: () =>
        {
            return Dp.ProcessEvent<UserLicense>(new UserLicenseGetByID());
        });
        return result;
    }
    private void ValidFields()
    {
        if (UserID == Guid.Empty)
            Dp.Notifications.Add("UserID is required");
        if (LicenseID == Guid.Empty)
            Dp.Notifications.Add("LicenseID is required");
        Dp.Notifications.ValidateAndThrow();
    }
    public virtual void ValidateIfUserExists()
    {
        var userExists = Dp.ProcessEvent<bool>(new UserExists()
        {UserID = this.UserID});
        if (!userExists)
            Dp.Notifications.Add($"User not found!");
        Dp.Notifications.ValidateAndThrow();
    }
    public virtual void ValidateIfLicenseExists()
    {
        var licenseExists = Dp.ProcessEvent<bool>(new LicenseExists()
        {LicenseID = this.LicenseID});
        if (!licenseExists)
            Dp.Notifications.Add($"License not found!");
        Dp.Notifications.ValidateAndThrow();
    }
}