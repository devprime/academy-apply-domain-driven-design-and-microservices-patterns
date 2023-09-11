namespace Application.Services.UserLicense.Model;
public class DeleteUserLicenseEventDTO
{
    public Guid ID { get; set; }
    public Guid UserID { get; set; }
    public Guid LicenseID { get; set; }
}