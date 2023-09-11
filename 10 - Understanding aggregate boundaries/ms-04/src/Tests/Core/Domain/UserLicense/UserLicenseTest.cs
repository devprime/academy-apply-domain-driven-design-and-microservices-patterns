namespace Core.Tests;
public class UserLicenseTest
{
    public static Guid FixedID = new Guid("40e09032-5906-4edd-97a4-9ab7e53711af");
    public static Guid UserIDFixedID = new Guid("46f06d36-283d-406a-9b61-967361d8d5a0");
    public static Guid LicenseIDFixedID = new Guid("c5c48353-ceca-4003-bbe7-fc26897efb6d");

#region fixtures
    public static Domain.Aggregates.UserLicense.UserLicense Create_UserLicense_Required_Properties_OK(DpTest dpTest)
    {
        var userlicense = new Domain.Aggregates.UserLicense.UserLicense();
        dpTest.MockDpDomain(userlicense);
        dpTest.Set<Guid>(userlicense, "ID", FixedID);
        dpTest.Set<Guid>(userlicense, "UserID", UserIDFixedID);
        dpTest.Set<Guid>(userlicense, "LicenseID", LicenseIDFixedID);
        return userlicense;
    }

#endregion fixtures

#region add
    [Fact]
    [Trait("Aggregate", "Add")]
    [Trait("Aggregate", "Success")]
    public void Add_Required_properties_filled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        var userlicense = Create_UserLicense_Required_Properties_OK(dpTest);
        dpTest.MockDpProcessEvent<bool>(userlicense, "UserExists", true);
        dpTest.MockDpProcessEvent<bool>(userlicense, "LicenseExists", true);
        dpTest.MockDpProcessEvent<bool>(userlicense, "CreateUserLicense", true);
        dpTest.MockDpProcessEvent(userlicense, "UserLicenseCreated");
        //Act
        userlicense.Add();
        //Assert
        var domainevents = dpTest.GetDomainEvents(userlicense);
        Assert.True(domainevents[0] is UserExists);
        Assert.True(domainevents[1] is LicenseExists);
        Assert.True(domainevents[2] is CreateUserLicense);
        Assert.NotEqual(userlicense.ID, Guid.Empty);
        Assert.True(userlicense.IsNew);
        Assert.True(userlicense.Dp.Notifications.IsValid);
    }

#endregion add

#region update
    [Fact]
    [Trait("Aggregate", "Update")]
    [Trait("Aggregate", "Success")]
    public void Update_Required_properties_filled_Success()
    {
        //Arrange        
        var dpTest = new DpTest();
        var userlicense = Create_UserLicense_Required_Properties_OK(dpTest);
        dpTest.MockDpProcessEvent<bool>(userlicense, "UserExists", true);
        dpTest.MockDpProcessEvent<bool>(userlicense, "LicenseExists", true);
        dpTest.MockDpProcessEvent<bool>(userlicense, "UpdateUserLicense", true);
        dpTest.MockDpProcessEvent(userlicense, "UserLicenseUpdated");
        //Act
        userlicense.Update();
        //Assert
        var domainevents = dpTest.GetDomainEvents(userlicense);
        Assert.True(domainevents[0] is UserExists);
        Assert.True(domainevents[1] is LicenseExists);
        Assert.True(domainevents[2] is UpdateUserLicense);
        Assert.NotEqual(userlicense.ID, Guid.Empty);
        Assert.True(userlicense.Dp.Notifications.IsValid);
    }

#endregion update

#region delete
    [Fact]
    [Trait("Aggregate", "Delete")]
    [Trait("Aggregate", "Success")]
    public void Delete_IDFilled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        var userlicense = Create_UserLicense_Required_Properties_OK(dpTest);
        dpTest.MockDpProcessEvent<bool>(userlicense, "DeleteUserLicense", true);
        dpTest.MockDpProcessEvent(userlicense, "UserLicenseDeleted");
        //Act
        userlicense.Delete();
        //Assert
        var domainevents = dpTest.GetDomainEvents(userlicense);
        Assert.True(domainevents[0] is UserExists);
        Assert.True(domainevents[1] is LicenseExists);
        Assert.True(domainevents[2] is DeleteUserLicense);
    }

#endregion delete
}