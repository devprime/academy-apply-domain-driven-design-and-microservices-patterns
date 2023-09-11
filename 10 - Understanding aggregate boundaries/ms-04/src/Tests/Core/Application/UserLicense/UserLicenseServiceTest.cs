namespace Core.Tests;
public class UserLicenseServiceTest
{
    public Application.Services.UserLicense.Model.UserLicense SetupCommand(Action add, Action update, Action delete, DpTest dpTest)
    {
        var domainUserLicenseMock = new Mock<Domain.Aggregates.UserLicense.UserLicense>();
        domainUserLicenseMock.Setup((o) => o.Add()).Callback(add);
        domainUserLicenseMock.Setup((o) => o.Update()).Callback(update);
        domainUserLicenseMock.Setup((o) => o.Delete()).Callback(delete);
        var userlicense = domainUserLicenseMock.Object;
        dpTest.MockDpDomain(userlicense);
        var applicationUserLicenseMock = new Mock<Application.Services.UserLicense.Model.UserLicense>();
        applicationUserLicenseMock.Setup((o) => o.ToDomain()).Returns(userlicense);
        var applicationUserLicense = applicationUserLicenseMock.Object;
        return applicationUserLicense;
    }
    public IUserLicenseService SetupApplicationService(DpTest dpTest)
    {
        var state = new Mock<IUserLicenseState>().Object;
        var userlicenseService = new Application.Services.UserLicense.UserLicenseService(state, dpTest.MockDp());
        return userlicenseService;
    }
    [Fact]
    [Trait("ApplicationService", "UserLicenseService")]
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
        var userlicenseService = SetupApplicationService(dpTest);
        //Act
        userlicenseService.Add(command);
        //Assert
        Assert.True(addCalled);
    }
    [Fact]
    [Trait("ApplicationService", "UserLicenseService")]
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
        var userlicenseService = SetupApplicationService(dpTest);
        //Act
        userlicenseService.Update(command);
        //Assert
        Assert.True(updateCalled);
    }
    [Fact]
    [Trait("ApplicationService", "UserLicenseService")]
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
        var userlicenseService = SetupApplicationService(dpTest);
        //Act
        userlicenseService.Delete(command);
        //Assert
        Assert.True(deleteCalled);
    }
}