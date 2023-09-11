namespace Core.Tests;
public class DeleteLicenseEventHandlerTest
{
    public DeleteLicense Create_License_Object_OK(DpTest dpTest)
    {
        var license = LicenseTest.Create_License_Required_Properties_OK(dpTest);
        var deleteLicense = new DeleteLicense();
        dpTest.SetDomainEventObject(deleteLicense, license);
        return deleteLicense;
    }
    [Fact]
    [Trait("EventHandler", "DeleteLicenseEventHandler")]
    [Trait("EventHandler", "Success")]
    public void Handle_LicenseObjectFilled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        object parameter = null;
        var deleteLicense = Create_License_Object_OK(dpTest);
        var license = dpTest.GetDomainEventObject<Domain.Aggregates.License.License>(deleteLicense);
        var repositoryMock = new Mock<ILicenseRepository>();
        repositoryMock.Setup((o) => o.Delete(license.ID)).Returns(true).Callback(() =>
        {
            parameter = license;
        });
        var repository = repositoryMock.Object;
        var stateMock = new Mock<ILicenseState>();
        stateMock.SetupGet((o) => o.License).Returns(repository);
        var state = stateMock.Object;
        var deleteLicenseEventHandler = new Application.EventHandlers.License.DeleteLicenseEventHandler(state, dpTest.MockDp<ILicenseState>(state));
        //Act
        var result = deleteLicenseEventHandler.Handle(deleteLicense);
        //Assert
        Assert.Equal(parameter, license);
        Assert.Equal(result, true);
    }
}