namespace Application.Interfaces.Adapters.State;
public interface IUserLicenseState
{
    IUserLicenseRepository UserLicense { get; set; }
}