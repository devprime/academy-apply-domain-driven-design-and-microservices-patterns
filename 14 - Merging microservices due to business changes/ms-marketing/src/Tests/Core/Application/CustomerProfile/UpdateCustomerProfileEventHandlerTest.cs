namespace Core.Tests;
public class UpdateCustomerProfileEventHandlerTest
{
    public UpdateCustomerProfile Create_CustomerProfile_Object_OK(DpTest dpTest)
    {
        var customerprofile = CustomerProfileTest.Create_CustomerProfile_Required_Properties_OK(dpTest);
        var updateCustomerProfile = new UpdateCustomerProfile();
        dpTest.SetDomainEventObject(updateCustomerProfile, customerprofile);
        return updateCustomerProfile;
    }
    [Fact]
    [Trait("EventHandler", "UpdateCustomerProfileEventHandler")]
    [Trait("EventHandler", "Success")]
    public void Handle_CustomerProfileObjectFilled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        object parameter = null;
        var updateCustomerProfile = Create_CustomerProfile_Object_OK(dpTest);
        var customerprofile = dpTest.GetDomainEventObject<Domain.Aggregates.CustomerProfile.CustomerProfile>(updateCustomerProfile);
        var repositoryMock = new Mock<ICustomerProfileRepository>();
        repositoryMock.Setup((o) => o.Update(customerprofile)).Returns(true).Callback(() =>
        {
            parameter = customerprofile;
        });
        var repository = repositoryMock.Object;
        var stateMock = new Mock<ICustomerProfileState>();
        stateMock.SetupGet((o) => o.CustomerProfile).Returns(repository);
        var state = stateMock.Object;
        var updateCustomerProfileEventHandler = new Application.EventHandlers.CustomerProfile.UpdateCustomerProfileEventHandler(state, dpTest.MockDp<ICustomerProfileState>(state));
        //Act
        var result = updateCustomerProfileEventHandler.Handle(updateCustomerProfile);
        //Assert
        Assert.Equal(parameter, customerprofile);
        Assert.Equal(result, true);
    }
}