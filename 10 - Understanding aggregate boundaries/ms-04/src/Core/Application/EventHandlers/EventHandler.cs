namespace Application.EventHandlers;
public class EventHandler : IEventHandler
{
    public EventHandler(IHandler handler)
    {
        handler.Add<CreateLicense, CreateLicenseEventHandler>();
        handler.Add<DeleteLicense, DeleteLicenseEventHandler>();
        handler.Add<LicenseGetByID, LicenseGetByIDEventHandler>();
        handler.Add<LicenseGet, LicenseGetEventHandler>();
        handler.Add<UpdateLicense, UpdateLicenseEventHandler>();
        handler.Add<CreateUser, CreateUserEventHandler>();
        handler.Add<DeleteUser, DeleteUserEventHandler>();
        handler.Add<UpdateUser, UpdateUserEventHandler>();
        handler.Add<UserGetByID, UserGetByIDEventHandler>();
        handler.Add<UserGet, UserGetEventHandler>();
        handler.Add<CreateUserLicense, CreateUserLicenseEventHandler>();
        handler.Add<DeleteUserLicense, DeleteUserLicenseEventHandler>();
        handler.Add<LicenseExists, LicenseExistsEventHandler>();
        handler.Add<UpdateUserLicense, UpdateUserLicenseEventHandler>();
        handler.Add<UserExists, UserExistsEventHandler>();
        handler.Add<UserLicenseGetByID, UserLicenseGetByIDEventHandler>();
        handler.Add<UserLicenseGet, UserLicenseGetEventHandler>();
    }
}