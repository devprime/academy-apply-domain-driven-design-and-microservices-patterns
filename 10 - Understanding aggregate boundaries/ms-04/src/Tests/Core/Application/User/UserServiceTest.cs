namespace Core.Tests;
public class UserServiceTest
{
    public Application.Services.User.Model.User SetupCommand(Action add, Action update, Action delete, DpTest dpTest)
    {
        var domainUserMock = new Mock<Domain.Aggregates.User.User>();
        domainUserMock.Setup((o) => o.Add()).Callback(add);
        domainUserMock.Setup((o) => o.Update()).Callback(update);
        domainUserMock.Setup((o) => o.Delete()).Callback(delete);
        var user = domainUserMock.Object;
        dpTest.MockDpDomain(user);
        dpTest.Set<string>(user, "Name", Faker.Lorem.Sentence(1));
        var applicationUserMock = new Mock<Application.Services.User.Model.User>();
        applicationUserMock.Setup((o) => o.ToDomain()).Returns(user);
        var applicationUser = applicationUserMock.Object;
        return applicationUser;
    }
    public IUserService SetupApplicationService(DpTest dpTest)
    {
        var state = new Mock<IUserState>().Object;
        var userService = new Application.Services.User.UserService(state, dpTest.MockDp());
        return userService;
    }
    [Fact]
    [Trait("ApplicationService", "UserService")]
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
        var userService = SetupApplicationService(dpTest);
        //Act
        userService.Add(command);
        //Assert
        Assert.True(addCalled);
    }
    [Fact]
    [Trait("ApplicationService", "UserService")]
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
        var userService = SetupApplicationService(dpTest);
        //Act
        userService.Update(command);
        //Assert
        Assert.True(updateCalled);
    }
    [Fact]
    [Trait("ApplicationService", "UserService")]
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
        var userService = SetupApplicationService(dpTest);
        //Act
        userService.Delete(command);
        //Assert
        Assert.True(deleteCalled);
    }
}