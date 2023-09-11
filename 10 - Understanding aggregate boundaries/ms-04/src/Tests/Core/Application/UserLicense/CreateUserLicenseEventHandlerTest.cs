namespace Core.Tests;
public class CreateUserLicenseEventHandlerTest
{
    public CreateUserLicense Create_UserLicense_Object_OK(DpTest dpTest)
    {
        var userlicense = UserLicenseTest.Create_UserLicense_Required_Properties_OK(dpTest);
        var createUserLicense = new CreateUserLicense();
        dpTest.SetDomainEventObject(createUserLicense, userlicense);
        return createUserLicense;
    }
    [Fact]
    [Trait("EventHandler", "CreateUserLicenseEventHandler")]
    [Trait("EventHandler", "Success")]
    public void Handle_UserLicenseObjectFilled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        object parameter = null;
        var createUserLicense = Create_UserLicense_Object_OK(dpTest);
        var userlicense = dpTest.GetDomainEventObject<Domain.Aggregates.UserLicense.UserLicense>(createUserLicense);
        var repositoryMock = new Mock<IUserLicenseRepository>();
        repositoryMock.Setup((o) => o.Add(userlicense)).Returns(true).Callback(() =>
        {
            parameter = userlicense;
        });
        var repository = repositoryMock.Object;
        var stateMock = new Mock<IUserLicenseState>();
        stateMock.SetupGet((o) => o.UserLicense).Returns(repository);
        var state = stateMock.Object;
        var createUserLicenseEventHandler = new Application.EventHandlers.UserLicense.CreateUserLicenseEventHandler(state, dpTest.MockDp<IUserLicenseState>(state));
        //Act
        var result = createUserLicenseEventHandler.Handle(createUserLicense);
        //Assert
        Assert.Equal(parameter, userlicense);
        Assert.Equal(result, true);
    }
}