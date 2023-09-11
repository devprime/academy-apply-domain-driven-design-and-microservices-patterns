namespace Core.Tests;
public class UpdateUserEventHandlerTest
{
    public UpdateUser Create_User_Object_OK(DpTest dpTest)
    {
        var user = UserTest.Create_User_Required_Properties_OK(dpTest);
        var updateUser = new UpdateUser();
        dpTest.SetDomainEventObject(updateUser, user);
        return updateUser;
    }
    [Fact]
    [Trait("EventHandler", "UpdateUserEventHandler")]
    [Trait("EventHandler", "Success")]
    public void Handle_UserObjectFilled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        object parameter = null;
        var updateUser = Create_User_Object_OK(dpTest);
        var user = dpTest.GetDomainEventObject<Domain.Aggregates.User.User>(updateUser);
        var repositoryMock = new Mock<IUserRepository>();
        repositoryMock.Setup((o) => o.Update(user)).Returns(true).Callback(() =>
        {
            parameter = user;
        });
        var repository = repositoryMock.Object;
        var stateMock = new Mock<IUserState>();
        stateMock.SetupGet((o) => o.User).Returns(repository);
        var state = stateMock.Object;
        var updateUserEventHandler = new Application.EventHandlers.User.UpdateUserEventHandler(state, dpTest.MockDp<IUserState>(state));
        //Act
        var result = updateUserEventHandler.Handle(updateUser);
        //Assert
        Assert.Equal(parameter, user);
        Assert.Equal(result, true);
    }
}