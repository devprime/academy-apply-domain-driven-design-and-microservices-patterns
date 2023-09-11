namespace Core.Tests;
public class CreateCustomerProfileEventHandlerTest
{
    public CreateCustomerProfile Create_CustomerProfile_Object_OK(DpTest dpTest)
    {
        var customerprofile = CustomerProfileTest.Create_CustomerProfile_Required_Properties_OK(dpTest);
        var createCustomerProfile = new CreateCustomerProfile();
        dpTest.SetDomainEventObject(createCustomerProfile, customerprofile);
        return createCustomerProfile;
    }
    [Fact]
    [Trait("EventHandler", "CreateCustomerProfileEventHandler")]
    [Trait("EventHandler", "Success")]
    public void Handle_CustomerProfileObjectFilled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        object parameter = null;
        var createCustomerProfile = Create_CustomerProfile_Object_OK(dpTest);
        var customerprofile = dpTest.GetDomainEventObject<Domain.Aggregates.CustomerProfile.CustomerProfile>(createCustomerProfile);
        var repositoryMock = new Mock<ICustomerProfileRepository>();
        repositoryMock.Setup((o) => o.Add(customerprofile)).Returns(true).Callback(() =>
        {
            parameter = customerprofile;
        });
        var repository = repositoryMock.Object;
        var stateMock = new Mock<ICustomerProfileState>();
        stateMock.SetupGet((o) => o.CustomerProfile).Returns(repository);
        var state = stateMock.Object;
        var createCustomerProfileEventHandler = new Application.EventHandlers.CustomerProfile.CreateCustomerProfileEventHandler(state, dpTest.MockDp<ICustomerProfileState>(state));
        //Act
        var result = createCustomerProfileEventHandler.Handle(createCustomerProfile);
        //Assert
        Assert.Equal(parameter, customerprofile);
        Assert.Equal(result, true);
    }
}