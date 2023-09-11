namespace Core.Tests;
public class UpdateCarEventHandlerTest
{
    public UpdateCar Create_Car_Object_OK(DpTest dpTest)
    {
        var car = CarTest.Create_Car_Required_Properties_OK(dpTest);
        var updateCar = new UpdateCar();
        dpTest.SetDomainEventObject(updateCar, car);
        return updateCar;
    }
    [Fact]
    [Trait("EventHandler", "UpdateCarEventHandler")]
    [Trait("EventHandler", "Success")]
    public void Handle_CarObjectFilled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        object parameter = null;
        var updateCar = Create_Car_Object_OK(dpTest);
        var car = dpTest.GetDomainEventObject<Domain.Aggregates.Car.Car>(updateCar);
        var repositoryMock = new Mock<ICarRepository>();
        repositoryMock.Setup((o) => o.Update(car)).Returns(true).Callback(() =>
        {
            parameter = car;
        });
        var repository = repositoryMock.Object;
        var stateMock = new Mock<ICarState>();
        stateMock.SetupGet((o) => o.Car).Returns(repository);
        var state = stateMock.Object;
        var updateCarEventHandler = new Application.EventHandlers.Car.UpdateCarEventHandler(state, dpTest.MockDp<ICarState>(state));
        //Act
        var result = updateCarEventHandler.Handle(updateCar);
        //Assert
        Assert.Equal(parameter, car);
        Assert.Equal(result, true);
    }
}