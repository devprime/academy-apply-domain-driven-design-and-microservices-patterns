namespace Core.Tests;
public class DeleteUserEventHandlerTest
{
    public DeleteUser Create_User_Object_OK(DpTest dpTest)
    {
        var user = UserTest.Create_User_Required_Properties_OK(dpTest);
        var deleteUser = new DeleteUser();
        dpTest.SetDomainEventObject(deleteUser, user);
        return deleteUser;
    }
    [Fact]
    [Trait("EventHandler", "DeleteUserEventHandler")]
    [Trait("EventHandler", "Success")]
    public void Handle_UserObjectFilled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        object parameter = null;
        var deleteUser = Create_User_Object_OK(dpTest);
        var user = dpTest.GetDomainEventObject<Domain.Aggregates.User.User>(deleteUser);
        var repositoryMock = new Mock<IUserRepository>();
        repositoryMock.Setup((o) => o.Delete(user.ID)).Returns(true).Callback(() =>
        {
            parameter = user;
        });
        var repository = repositoryMock.Object;
        var stateMock = new Mock<IUserState>();
        stateMock.SetupGet((o) => o.User).Returns(repository);
        var state = stateMock.Object;
        var deleteUserEventHandler = new Application.EventHandlers.User.DeleteUserEventHandler(state, dpTest.MockDp<IUserState>(state));
        //Act
        var result = deleteUserEventHandler.Handle(deleteUser);
        //Assert
        Assert.Equal(parameter, user);
        Assert.Equal(result, true);
    }
}