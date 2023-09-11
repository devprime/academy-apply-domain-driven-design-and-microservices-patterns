namespace DevPrime.State.States;
public class LicenseState : ILicenseState
{
    public ILicenseRepository License { get; set; }
    public LicenseState(ILicenseRepository license)
    {
        License = license;
    }
}