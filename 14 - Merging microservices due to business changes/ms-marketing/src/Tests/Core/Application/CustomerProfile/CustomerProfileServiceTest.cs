namespace Core.Tests;
public class CustomerProfileServiceTest
{
    public Application.Services.CustomerProfile.Model.CustomerProfile SetupCommand(Action add, Action update, Action delete, DpTest dpTest)
    {
        var domainCustomerProfileMock = new Mock<Domain.Aggregates.CustomerProfile.CustomerProfile>();
        domainCustomerProfileMock.Setup((o) => o.Add()).Callback(add);
        domainCustomerProfileMock.Setup((o) => o.Update()).Callback(update);
        domainCustomerProfileMock.Setup((o) => o.Delete()).Callback(delete);
        var customerprofile = domainCustomerProfileMock.Object;
        dpTest.MockDpDomain(customerprofile);
        dpTest.Set<string>(customerprofile, "Email", Faker.Lorem.Sentence(1));
        dpTest.Set<string>(customerprofile, "Name", Faker.Lorem.Sentence(1));
        dpTest.Set<string>(customerprofile, "Photo", Faker.Lorem.Sentence(1));
        dpTest.Set<DateTime>(customerprofile, "BirthDate", DateTime.Now);
        var applicationCustomerProfileMock = new Mock<Application.Services.CustomerProfile.Model.CustomerProfile>();
        applicationCustomerProfileMock.Setup((o) => o.ToDomain()).Returns(customerprofile);
        var applicationCustomerProfile = applicationCustomerProfileMock.Object;
        return applicationCustomerProfile;
    }
    public ICustomerProfileService SetupApplicationService(DpTest dpTest)
    {
        var state = new Mock<ICustomerProfileState>().Object;
        var customerprofileService = new Application.Services.CustomerProfile.CustomerProfileService(state, dpTest.MockDp());
        return customerprofileService;
    }
    [Fact]
    [Trait("ApplicationService", "CustomerProfileService")]
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
        var customerprofileService = SetupApplicationService(dpTest);
        //Act
        customerprofileService.Add(command);
        //Assert
        Assert.True(addCalled);
    }
    [Fact]
    [Trait("ApplicationService", "CustomerProfileService")]
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
        var customerprofileService = SetupApplicationService(dpTest);
        //Act
        customerprofileService.Update(command);
        //Assert
        Assert.True(updateCalled);
    }
    [Fact]
    [Trait("ApplicationService", "CustomerProfileService")]
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
        var customerprofileService = SetupApplicationService(dpTest);
        //Act
        customerprofileService.Delete(command);
        //Assert
        Assert.True(deleteCalled);
    }
}