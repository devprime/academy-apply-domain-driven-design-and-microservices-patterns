namespace DevPrime.State.States;
public class UserLicenseState : IUserLicenseState
{
    public IUserLicenseRepository UserLicense { get; set; }
    public UserLicenseState(IUserLicenseRepository userLicense)
    {
        UserLicense = userLicense;
    }
}