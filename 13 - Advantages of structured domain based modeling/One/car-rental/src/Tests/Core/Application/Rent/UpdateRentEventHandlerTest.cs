namespace Core.Tests;
public class UpdateRentEventHandlerTest
{
    public UpdateRent Create_Rent_Object_OK(DpTest dpTest)
    {
        var rent = RentTest.Create_Rent_Required_Properties_OK(dpTest);
        var updateRent = new UpdateRent();
        dpTest.SetDomainEventObject(updateRent, rent);
        return updateRent;
    }
    [Fact]
    [Trait("EventHandler", "UpdateRentEventHandler")]
    [Trait("EventHandler", "Success")]
    public void Handle_RentObjectFilled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        object parameter = null;
        var updateRent = Create_Rent_Object_OK(dpTest);
        var rent = dpTest.GetDomainEventObject<Domain.Aggregates.Rent.Rent>(updateRent);
        var repositoryMock = new Mock<IRentRepository>();
        repositoryMock.Setup((o) => o.Update(rent)).Returns(true).Callback(() =>
        {
            parameter = rent;
        });
        var repository = repositoryMock.Object;
        var stateMock = new Mock<IRentState>();
        stateMock.SetupGet((o) => o.Rent).Returns(repository);
        var state = stateMock.Object;
        var updateRentEventHandler = new Application.EventHandlers.Rent.UpdateRentEventHandler(state, dpTest.MockDp<IRentState>(state));
        //Act
        var result = updateRentEventHandler.Handle(updateRent);
        //Assert
        Assert.Equal(parameter, rent);
        Assert.Equal(result, true);
    }
}