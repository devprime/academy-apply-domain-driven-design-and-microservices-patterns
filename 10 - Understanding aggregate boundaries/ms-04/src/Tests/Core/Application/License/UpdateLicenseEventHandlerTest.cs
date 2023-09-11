namespace Core.Tests;
public class UpdateLicenseEventHandlerTest
{
    public UpdateLicense Create_License_Object_OK(DpTest dpTest)
    {
        var license = LicenseTest.Create_License_Required_Properties_OK(dpTest);
        var updateLicense = new UpdateLicense();
        dpTest.SetDomainEventObject(updateLicense, license);
        return updateLicense;
    }
    [Fact]
    [Trait("EventHandler", "UpdateLicenseEventHandler")]
    [Trait("EventHandler", "Success")]
    public void Handle_LicenseObjectFilled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        object parameter = null;
        var updateLicense = Create_License_Object_OK(dpTest);
        var license = dpTest.GetDomainEventObject<Domain.Aggregates.License.License>(updateLicense);
        var repositoryMock = new Mock<ILicenseRepository>();
        repositoryMock.Setup((o) => o.Update(license)).Returns(true).Callback(() =>
        {
            parameter = license;
        });
        var repository = repositoryMock.Object;
        var stateMock = new Mock<ILicenseState>();
        stateMock.SetupGet((o) => o.License).Returns(repository);
        var state = stateMock.Object;
        var updateLicenseEventHandler = new Application.EventHandlers.License.UpdateLicenseEventHandler(state, dpTest.MockDp<ILicenseState>(state));
        //Act
        var result = updateLicenseEventHandler.Handle(updateLicense);
        //Assert
        Assert.Equal(parameter, license);
        Assert.Equal(result, true);
    }
}