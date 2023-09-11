namespace Core.Tests;
public class DeleteUserLicenseEventHandlerTest
{
    public DeleteUserLicense Create_UserLicense_Object_OK(DpTest dpTest)
    {
        var userlicense = UserLicenseTest.Create_UserLicense_Required_Properties_OK(dpTest);
        var deleteUserLicense = new DeleteUserLicense();
        dpTest.SetDomainEventObject(deleteUserLicense, userlicense);
        return deleteUserLicense;
    }
    [Fact]
    [Trait("EventHandler", "DeleteUserLicenseEventHandler")]
    [Trait("EventHandler", "Success")]
    public void Handle_UserLicenseObjectFilled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        object parameter = null;
        var deleteUserLicense = Create_UserLicense_Object_OK(dpTest);
        var userlicense = dpTest.GetDomainEventObject<Domain.Aggregates.UserLicense.UserLicense>(deleteUserLicense);
        var repositoryMock = new Mock<IUserLicenseRepository>();
        repositoryMock.Setup((o) => o.Delete(userlicense.ID)).Returns(true).Callback(() =>
        {
            parameter = userlicense;
        });
        var repository = repositoryMock.Object;
        var stateMock = new Mock<IUserLicenseState>();
        stateMock.SetupGet((o) => o.UserLicense).Returns(repository);
        var state = stateMock.Object;
        var deleteUserLicenseEventHandler = new Application.EventHandlers.UserLicense.DeleteUserLicenseEventHandler(state, dpTest.MockDp<IUserLicenseState>(state));
        //Act
        var result = deleteUserLicenseEventHandler.Handle(deleteUserLicense);
        //Assert
        Assert.Equal(parameter, userlicense);
        Assert.Equal(result, true);
    }
}