namespace Core.Tests;
public class CreateCarEventHandlerTest
{
    public CreateCar Create_Car_Object_OK(DpTest dpTest)
    {
        var car = CarTest.Create_Car_Required_Properties_OK(dpTest);
        var createCar = new CreateCar();
        dpTest.SetDomainEventObject(createCar, car);
        return createCar;
    }
    [Fact]
    [Trait("EventHandler", "CreateCarEventHandler")]
    [Trait("EventHandler", "Success")]
    public void Handle_CarObjectFilled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        object parameter = null;
        var createCar = Create_Car_Object_OK(dpTest);
        var car = dpTest.GetDomainEventObject<Domain.Aggregates.Car.Car>(createCar);
        var repositoryMock = new Mock<ICarRepository>();
        repositoryMock.Setup((o) => o.Add(car)).Returns(true).Callback(() =>
        {
            parameter = car;
        });
        var repository = repositoryMock.Object;
        var stateMock = new Mock<ICarState>();
        stateMock.SetupGet((o) => o.Car).Returns(repository);
        var state = stateMock.Object;
        var createCarEventHandler = new Application.EventHandlers.Car.CreateCarEventHandler(state, dpTest.MockDp<ICarState>(state));
        //Act
        var result = createCarEventHandler.Handle(createCar);
        //Assert
        Assert.Equal(parameter, car);
        Assert.Equal(result, true);
    }
}