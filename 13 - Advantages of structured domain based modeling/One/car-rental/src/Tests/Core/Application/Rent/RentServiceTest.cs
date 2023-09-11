namespace Core.Tests;
public class RentServiceTest
{
    public Application.Services.Rent.Model.Rent SetupCommand(Action add, Action update, Action delete, DpTest dpTest)
    {
        var domainRentMock = new Mock<Domain.Aggregates.Rent.Rent>();
        domainRentMock.Setup((o) => o.Add()).Callback(add);
        domainRentMock.Setup((o) => o.Update()).Callback(update);
        domainRentMock.Setup((o) => o.Delete()).Callback(delete);
        var rent = domainRentMock.Object;
        dpTest.MockDpDomain(rent);
        dpTest.Set<string>(rent, "LicensePlate", Faker.Lorem.Sentence(1));
        dpTest.Set<string>(rent, "TaxID", Faker.Lorem.Sentence(1));
        dpTest.Set<DateTime>(rent, "Start", DateTime.Now);
        dpTest.Set<DateTime>(rent, "End", DateTime.Now);
        var applicationRentMock = new Mock<Application.Services.Rent.Model.Rent>();
        applicationRentMock.Setup((o) => o.ToDomain()).Returns(rent);
        var applicationRent = applicationRentMock.Object;
        return applicationRent;
    }
    public IRentService SetupApplicationService(DpTest dpTest)
    {
        var state = new Mock<IRentState>().Object;
        var rentService = new Application.Services.Rent.RentService(state, dpTest.MockDp());
        return rentService;
    }
    [Fact]
    [Trait("ApplicationService", "RentService")]
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
        var rentService = SetupApplicationService(dpTest);
        //Act
        rentService.Add(command);
        //Assert
        Assert.True(addCalled);
    }
    [Fact]
    [Trait("ApplicationService", "RentService")]
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
        var rentService = SetupApplicationService(dpTest);
        //Act
        rentService.Update(command);
        //Assert
        Assert.True(updateCalled);
    }
    [Fact]
    [Trait("ApplicationService", "RentService")]
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
        var rentService = SetupApplicationService(dpTest);
        //Act
        rentService.Delete(command);
        //Assert
        Assert.True(deleteCalled);
    }
}