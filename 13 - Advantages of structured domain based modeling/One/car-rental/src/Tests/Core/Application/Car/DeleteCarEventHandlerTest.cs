namespace Core.Tests;
public class DeleteCarEventHandlerTest
{
    public DeleteCar Create_Car_Object_OK(DpTest dpTest)
    {
        var car = CarTest.Create_Car_Required_Properties_OK(dpTest);
        var deleteCar = new DeleteCar();
        dpTest.SetDomainEventObject(deleteCar, car);
        return deleteCar;
    }
    [Fact]
    [Trait("EventHandler", "DeleteCarEventHandler")]
    [Trait("EventHandler", "Success")]
    public void Handle_CarObjectFilled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        object parameter = null;
        var deleteCar = Create_Car_Object_OK(dpTest);
        var car = dpTest.GetDomainEventObject<Domain.Aggregates.Car.Car>(deleteCar);
        var repositoryMock = new Mock<ICarRepository>();
        repositoryMock.Setup((o) => o.Delete(car.ID)).Returns(true).Callback(() =>
        {
            parameter = car;
        });
        var repository = repositoryMock.Object;
        var stateMock = new Mock<ICarState>();
        stateMock.SetupGet((o) => o.Car).Returns(repository);
        var state = stateMock.Object;
        var deleteCarEventHandler = new Application.EventHandlers.Car.DeleteCarEventHandler(state, dpTest.MockDp<ICarState>(state));
        //Act
        var result = deleteCarEventHandler.Handle(deleteCar);
        //Assert
        Assert.Equal(parameter, car);
        Assert.Equal(result, true);
    }
}