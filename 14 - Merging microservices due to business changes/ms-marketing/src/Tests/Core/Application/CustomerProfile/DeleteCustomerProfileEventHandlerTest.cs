namespace Core.Tests;
public class DeleteCustomerProfileEventHandlerTest
{
    public DeleteCustomerProfile Create_CustomerProfile_Object_OK(DpTest dpTest)
    {
        var customerprofile = CustomerProfileTest.Create_CustomerProfile_Required_Properties_OK(dpTest);
        var deleteCustomerProfile = new DeleteCustomerProfile();
        dpTest.SetDomainEventObject(deleteCustomerProfile, customerprofile);
        return deleteCustomerProfile;
    }
    [Fact]
    [Trait("EventHandler", "DeleteCustomerProfileEventHandler")]
    [Trait("EventHandler", "Success")]
    public void Handle_CustomerProfileObjectFilled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        object parameter = null;
        var deleteCustomerProfile = Create_CustomerProfile_Object_OK(dpTest);
        var customerprofile = dpTest.GetDomainEventObject<Domain.Aggregates.CustomerProfile.CustomerProfile>(deleteCustomerProfile);
        var repositoryMock = new Mock<ICustomerProfileRepository>();
        repositoryMock.Setup((o) => o.Delete(customerprofile.ID)).Returns(true).Callback(() =>
        {
            parameter = customerprofile;
        });
        var repository = repositoryMock.Object;
        var stateMock = new Mock<ICustomerProfileState>();
        stateMock.SetupGet((o) => o.CustomerProfile).Returns(repository);
        var state = stateMock.Object;
        var deleteCustomerProfileEventHandler = new Application.EventHandlers.CustomerProfile.DeleteCustomerProfileEventHandler(state, dpTest.MockDp<ICustomerProfileState>(state));
        //Act
        var result = deleteCustomerProfileEventHandler.Handle(deleteCustomerProfile);
        //Assert
        Assert.Equal(parameter, customerprofile);
        Assert.Equal(result, true);
    }
}