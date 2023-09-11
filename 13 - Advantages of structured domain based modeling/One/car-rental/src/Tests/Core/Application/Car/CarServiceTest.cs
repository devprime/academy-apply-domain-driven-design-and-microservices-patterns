namespace Core.Tests;
public class CarServiceTest
{
    public Application.Services.Car.Model.Car SetupCommand(Action add, Action update, Action delete, DpTest dpTest)
    {
        var domainCarMock = new Mock<Domain.Aggregates.Car.Car>();
        domainCarMock.Setup((o) => o.Add()).Callback(add);
        domainCarMock.Setup((o) => o.Update()).Callback(update);
        domainCarMock.Setup((o) => o.Delete()).Callback(delete);
        var car = domainCarMock.Object;
        dpTest.MockDpDomain(car);
        dpTest.Set<string>(car, "Model", Faker.Lorem.Sentence(1));
        dpTest.Set<string>(car, "LicensePlate", Faker.Lorem.Sentence(1));
        var applicationCarMock = new Mock<Application.Services.Car.Model.Car>();
        applicationCarMock.Setup((o) => o.ToDomain()).Returns(car);
        var applicationCar = applicationCarMock.Object;
        return applicationCar;
    }
    public ICarService SetupApplicationService(DpTest dpTest)
    {
        var state = new Mock<ICarState>().Object;
        var carService = new Application.Services.Car.CarService(state, dpTest.MockDp());
        return carService;
    }
    [Fact]
    [Trait("ApplicationService", "CarService")]
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
        var carService = SetupApplicationService(dpTest);
        //Act
        carService.Add(command);
        //Assert
        Assert.True(addCalled);
    }
    [Fact]
    [Trait("ApplicationService", "CarService")]
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
        var carService = SetupApplicationService(dpTest);
        //Act
        carService.Update(command);
        //Assert
        Assert.True(updateCalled);
    }
    [Fact]
    [Trait("ApplicationService", "CarService")]
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
        var carService = SetupApplicationService(dpTest);
        //Act
        carService.Delete(command);
        //Assert
        Assert.True(deleteCalled);
    }
}