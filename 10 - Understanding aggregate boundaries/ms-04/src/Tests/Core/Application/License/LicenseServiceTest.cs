namespace Core.Tests;
public class LicenseServiceTest
{
    public Application.Services.License.Model.License SetupCommand(Action add, Action update, Action delete, DpTest dpTest)
    {
        var domainLicenseMock = new Mock<Domain.Aggregates.License.License>();
        domainLicenseMock.Setup((o) => o.Add()).Callback(add);
        domainLicenseMock.Setup((o) => o.Update()).Callback(update);
        domainLicenseMock.Setup((o) => o.Delete()).Callback(delete);
        var license = domainLicenseMock.Object;
        dpTest.MockDpDomain(license);
        dpTest.Set<string>(license, "Description", Faker.Lorem.Sentence(1));
        var applicationLicenseMock = new Mock<Application.Services.License.Model.License>();
        applicationLicenseMock.Setup((o) => o.ToDomain()).Returns(license);
        var applicationLicense = applicationLicenseMock.Object;
        return applicationLicense;
    }
    public ILicenseService SetupApplicationService(DpTest dpTest)
    {
        var state = new Mock<ILicenseState>().Object;
        var licenseService = new Application.Services.License.LicenseService(state, dpTest.MockDp());
        return licenseService;
    }
    [Fact]
    [Trait("ApplicationService", "LicenseService")]
    [Trait("ApplicationService", "Success")]
    public void Add_CommandNotNull_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        var addCalled = false;
        var add = () =>
        {
            addCalled = true;
        };
        var command = SetupCommand(add, () =>
        {
        }, () =>
        {
        }, dpTest);
        var licenseService = SetupApplicationService(dpTest);
        //Act
        licenseService.Add(command);
        //Assert
        Assert.True(addCalled);
    }
    [Fact]
    [Trait("ApplicationService", "LicenseService")]
    [Trait("ApplicationService", "Success")]
    public void Update_CommandFilled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        var updateCalled = false;
        var update = () =>
        {
            updateCalled = true;
        };
        var command = SetupCommand(() =>
        {
        }, update, () =>
        {
        }, dpTest);
        var licenseService = SetupApplicationService(dpTest);
        //Act
        licenseService.Update(command);
        //Assert
        Assert.True(updateCalled);
    }
    [Fact]
    [Trait("ApplicationService", "LicenseService")]
    [Trait("ApplicationService", "Success")]
    public void Delete_CommandFilled_Success()
    {
        //Arrange        
        var dpTest = new DpTest();
        var deleteCalled = false;
        var delete = () =>
        {
            deleteCalled = true;
        };
        var command = SetupCommand(() =>
        {
        }, () =>
        {
        }, delete, dpTest);
        var licenseService = SetupApplicationService(dpTest);
        //Act
        licenseService.Delete(command);
        //Assert
        Assert.True(deleteCalled);
    }
}