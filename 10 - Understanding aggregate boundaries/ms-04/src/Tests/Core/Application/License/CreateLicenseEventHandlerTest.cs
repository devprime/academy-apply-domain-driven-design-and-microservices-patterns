namespace Core.Tests;
public class CreateLicenseEventHandlerTest
{
    public CreateLicense Create_License_Object_OK(DpTest dpTest)
    {
        var license = LicenseTest.Create_License_Required_Properties_OK(dpTest);
        var createLicense = new CreateLicense();
        dpTest.SetDomainEventObject(createLicense, license);
        return createLicense;
    }
    [Fact]
    [Trait("EventHandler", "CreateLicenseEventHandler")]
    [Trait("EventHandler", "Success")]
    public void Handle_LicenseObjectFilled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        object parameter = null;
        var createLicense = Create_License_Object_OK(dpTest);
        var license = dpTest.GetDomainEventObject<Domain.Aggregates.License.License>(createLicense);
        var repositoryMock = new Mock<ILicenseRepository>();
        repositoryMock.Setup((o) => o.Add(license)).Returns(true).Callback(() =>
        {
            parameter = license;
        });
        var repository = repositoryMock.Object;
        var stateMock = new Mock<ILicenseState>();
        stateMock.SetupGet((o) => o.License).Returns(repository);
        var state = stateMock.Object;
        var createLicenseEventHandler = new Application.EventHandlers.License.CreateLicenseEventHandler(state, dpTest.MockDp<ILicenseState>(state));
        //Act
        var result = createLicenseEventHandler.Handle(createLicense);
        //Assert
        Assert.Equal(parameter, license);
        Assert.Equal(result, true);
    }
}