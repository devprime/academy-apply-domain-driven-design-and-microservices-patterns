namespace Core.Tests;
public class DeleteRentEventHandlerTest
{
    public DeleteRent Create_Rent_Object_OK(DpTest dpTest)
    {
        var rent = RentTest.Create_Rent_Required_Properties_OK(dpTest);
        var deleteRent = new DeleteRent();
        dpTest.SetDomainEventObject(deleteRent, rent);
        return deleteRent;
    }
    [Fact]
    [Trait("EventHandler", "DeleteRentEventHandler")]
    [Trait("EventHandler", "Success")]
    public void Handle_RentObjectFilled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        object parameter = null;
        var deleteRent = Create_Rent_Object_OK(dpTest);
        var rent = dpTest.GetDomainEventObject<Domain.Aggregates.Rent.Rent>(deleteRent);
        var repositoryMock = new Mock<IRentRepository>();
        repositoryMock.Setup((o) => o.Delete(rent.ID)).Returns(true).Callback(() =>
        {
            parameter = rent;
        });
        var repository = repositoryMock.Object;
        var stateMock = new Mock<IRentState>();
        stateMock.SetupGet((o) => o.Rent).Returns(repository);
        var state = stateMock.Object;
        var deleteRentEventHandler = new Application.EventHandlers.Rent.DeleteRentEventHandler(state, dpTest.MockDp<IRentState>(state));
        //Act
        var result = deleteRentEventHandler.Handle(deleteRent);
        //Assert
        Assert.Equal(parameter, rent);
        Assert.Equal(result, true);
    }
}