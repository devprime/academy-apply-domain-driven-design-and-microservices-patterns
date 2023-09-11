namespace Core.Tests;
public class PromoCodeServiceTest
{
    public Application.Services.PromoCode.Model.PromoCode SetupCommand(Action add, Action update, Action delete, DpTest dpTest)
    {
        var domainPromoCodeMock = new Mock<Domain.Aggregates.PromoCode.PromoCode>();
        domainPromoCodeMock.Setup((o) => o.Add()).Callback(add);
        domainPromoCodeMock.Setup((o) => o.Update()).Callback(update);
        domainPromoCodeMock.Setup((o) => o.Delete()).Callback(delete);
        var promocode = domainPromoCodeMock.Object;
        dpTest.MockDpDomain(promocode);
        dpTest.Set<string>(promocode, "Code", Faker.Lorem.Sentence(1));
        dpTest.Set<DateTime>(promocode, "ValidUntil", DateTime.Now);
        var applicationPromoCodeMock = new Mock<Application.Services.PromoCode.Model.PromoCode>();
        applicationPromoCodeMock.Setup((o) => o.ToDomain()).Returns(promocode);
        var applicationPromoCode = applicationPromoCodeMock.Object;
        return applicationPromoCode;
    }
    public IPromoCodeService SetupApplicationService(DpTest dpTest)
    {
        var state = new Mock<IPromoCodeState>().Object;
        var promocodeService = new Application.Services.PromoCode.PromoCodeService(state, dpTest.MockDp());
        return promocodeService;
    }
    [Fact]
    [Trait("ApplicationService", "PromoCodeService")]
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
        var promocodeService = SetupApplicationService(dpTest);
        //Act
        promocodeService.Add(command);
        //Assert
        Assert.True(addCalled);
    }
    [Fact]
    [Trait("ApplicationService", "PromoCodeService")]
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
        var promocodeService = SetupApplicationService(dpTest);
        //Act
        promocodeService.Update(command);
        //Assert
        Assert.True(updateCalled);
    }
    [Fact]
    [Trait("ApplicationService", "PromoCodeService")]
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
        var promocodeService = SetupApplicationService(dpTest);
        //Act
        promocodeService.Delete(command);
        //Assert
        Assert.True(deleteCalled);
    }
}