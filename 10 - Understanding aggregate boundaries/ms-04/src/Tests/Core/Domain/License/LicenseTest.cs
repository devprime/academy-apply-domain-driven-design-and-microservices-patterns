namespace Core.Tests;
public class LicenseTest
{
    public static Guid FixedID = new Guid("a514746b-0eb1-4051-bcbd-325fefedaafd");

#region fixtures
    public static Domain.Aggregates.License.License Create_License_Required_Properties_OK(DpTest dpTest)
    {
        var license = new Domain.Aggregates.License.License();
        dpTest.MockDpDomain(license);
        dpTest.Set<Guid>(license, "ID", FixedID);
        dpTest.Set<string>(license, "Description", Faker.Lorem.Sentence(1));
        dpTest.Set<Domain.Aggregates.License.LicenseType>(license, "Type", default(Domain.Aggregates.License.LicenseType));
        dpTest.Set<string>(license, "typeValue", "Developer");
        return license;
    }
    public static Domain.Aggregates.License.License Create_License_With_Description_Required_Property_Missing(DpTest dpTest)
    {
        var license = new Domain.Aggregates.License.License();
        dpTest.MockDpDomain(license);
        dpTest.Set<Guid>(license, "ID", FixedID);
        dpTest.Set<Domain.Aggregates.License.LicenseType>(license, "Type", default(Domain.Aggregates.License.LicenseType));
        dpTest.Set<string>(license, "typeValue", "Developer");
        return license;
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
        var license = Create_License_Required_Properties_OK(dpTest);
        dpTest.MockDpProcessEvent<bool>(license, "CreateLicense", true);
        dpTest.MockDpProcessEvent(license, "LicenseCreated");
        //Act
        license.Add();
        //Assert
        var domainevents = dpTest.GetDomainEvents(license);
        Assert.True(domainevents[0] is CreateLicense);
        Assert.NotEqual(license.ID, Guid.Empty);
        Assert.True(license.IsNew);
        Assert.True(license.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Add")]
    [Trait("Aggregate", "Fail")]
    public void Add_Description_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var license = Create_License_With_Description_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(license.Add);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("Description is required", i));
        Assert.False(license.Dp.Notifications.IsValid);
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
        var license = Create_License_Required_Properties_OK(dpTest);
        dpTest.MockDpProcessEvent<bool>(license, "UpdateLicense", true);
        dpTest.MockDpProcessEvent(license, "LicenseUpdated");
        //Act
        license.Update();
        //Assert
        var domainevents = dpTest.GetDomainEvents(license);
        Assert.True(domainevents[0] is UpdateLicense);
        Assert.NotEqual(license.ID, Guid.Empty);
        Assert.True(license.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Update")]
    [Trait("Aggregate", "Fail")]
    public void Update_Description_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var license = Create_License_With_Description_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(license.Update);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("Description is required", i));
        Assert.False(license.Dp.Notifications.IsValid);
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
        var license = Create_License_Required_Properties_OK(dpTest);
        dpTest.MockDpProcessEvent<bool>(license, "DeleteLicense", true);
        dpTest.MockDpProcessEvent(license, "LicenseDeleted");
        //Act
        license.Delete();
        //Assert
        var domainevents = dpTest.GetDomainEvents(license);
        Assert.True(domainevents[0] is DeleteLicense);
    }

#endregion delete
}