namespace Core.Tests;
public class CreateRentEventHandlerTest
{
    public CreateRent Create_Rent_Object_OK(DpTest dpTest)
    {
        var rent = RentTest.Create_Rent_Required_Properties_OK(dpTest);
        var createRent = new CreateRent();
        dpTest.SetDomainEventObject(createRent, rent);
        return createRent;
    }
    [Fact]
    [Trait("EventHandler", "CreateRentEventHandler")]
    [Trait("EventHandler", "Success")]
    public void Handle_RentObjectFilled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        object parameter = null;
        var createRent = Create_Rent_Object_OK(dpTest);
        var rent = dpTest.GetDomainEventObject<Domain.Aggregates.Rent.Rent>(createRent);
        var repositoryMock = new Mock<IRentRepository>();
        repositoryMock.Setup((o) => o.Add(rent)).Returns(true).Callback(() =>
        {
            parameter = rent;
        });
        var repository = repositoryMock.Object;
        var stateMock = new Mock<IRentState>();
        stateMock.SetupGet((o) => o.Rent).Returns(repository);
        var state = stateMock.Object;
        var createRentEventHandler = new Application.EventHandlers.Rent.CreateRentEventHandler(state, dpTest.MockDp<IRentState>(state));
        //Act
        var result = createRentEventHandler.Handle(createRent);
        //Assert
        Assert.Equal(parameter, rent);
        Assert.Equal(result, true);
    }
}