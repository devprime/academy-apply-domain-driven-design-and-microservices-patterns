namespace Core.Tests;
public class UpdateUserLicenseEventHandlerTest
{
    public UpdateUserLicense Create_UserLicense_Object_OK(DpTest dpTest)
    {
        var userlicense = UserLicenseTest.Create_UserLicense_Required_Properties_OK(dpTest);
        var updateUserLicense = new UpdateUserLicense();
        dpTest.SetDomainEventObject(updateUserLicense, userlicense);
        return updateUserLicense;
    }
    [Fact]
    [Trait("EventHandler", "UpdateUserLicenseEventHandler")]
    [Trait("EventHandler", "Success")]
    public void Handle_UserLicenseObjectFilled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        object parameter = null;
        var updateUserLicense = Create_UserLicense_Object_OK(dpTest);
        var userlicense = dpTest.GetDomainEventObject<Domain.Aggregates.UserLicense.UserLicense>(updateUserLicense);
        var repositoryMock = new Mock<IUserLicenseRepository>();
        repositoryMock.Setup((o) => o.Update(userlicense)).Returns(true).Callback(() =>
        {
            parameter = userlicense;
        });
        var repository = repositoryMock.Object;
        var stateMock = new Mock<IUserLicenseState>();
        stateMock.SetupGet((o) => o.UserLicense).Returns(repository);
        var state = stateMock.Object;
        var updateUserLicenseEventHandler = new Application.EventHandlers.UserLicense.UpdateUserLicenseEventHandler(state, dpTest.MockDp<IUserLicenseState>(state));
        //Act
        var result = updateUserLicenseEventHandler.Handle(updateUserLicense);
        //Assert
        Assert.Equal(parameter, userlicense);
        Assert.Equal(result, true);
    }
}