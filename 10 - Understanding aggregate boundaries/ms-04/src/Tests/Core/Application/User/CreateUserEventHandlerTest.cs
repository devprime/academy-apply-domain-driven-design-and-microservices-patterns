namespace Core.Tests;
public class CreateUserEventHandlerTest
{
    public CreateUser Create_User_Object_OK(DpTest dpTest)
    {
        var user = UserTest.Create_User_Required_Properties_OK(dpTest);
        var createUser = new CreateUser();
        dpTest.SetDomainEventObject(createUser, user);
        return createUser;
    }
    [Fact]
    [Trait("EventHandler", "CreateUserEventHandler")]
    [Trait("EventHandler", "Success")]
    public void Handle_UserObjectFilled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        object parameter = null;
        var createUser = Create_User_Object_OK(dpTest);
        var user = dpTest.GetDomainEventObject<Domain.Aggregates.User.User>(createUser);
        var repositoryMock = new Mock<IUserRepository>();
        repositoryMock.Setup((o) => o.Add(user)).Returns(true).Callback(() =>
        {
            parameter = user;
        });
        var repository = repositoryMock.Object;
        var stateMock = new Mock<IUserState>();
        stateMock.SetupGet((o) => o.User).Returns(repository);
        var state = stateMock.Object;
        var createUserEventHandler = new Application.EventHandlers.User.CreateUserEventHandler(state, dpTest.MockDp<IUserState>(state));
        //Act
        var result = createUserEventHandler.Handle(createUser);
        //Assert
        Assert.Equal(parameter, user);
        Assert.Equal(result, true);
    }
}