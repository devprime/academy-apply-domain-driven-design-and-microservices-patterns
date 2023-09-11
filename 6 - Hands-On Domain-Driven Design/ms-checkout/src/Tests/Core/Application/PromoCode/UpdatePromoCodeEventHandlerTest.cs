namespace Core.Tests;
public class UpdatePromoCodeEventHandlerTest
{
    public UpdatePromoCode Create_PromoCode_Object_OK(DpTest dpTest)
    {
        var promocode = PromoCodeTest.Create_PromoCode_Required_Properties_OK(dpTest);
        var updatePromoCode = new UpdatePromoCode();
        dpTest.SetDomainEventObject(updatePromoCode, promocode);
        return updatePromoCode;
    }
    [Fact]
    [Trait("EventHandler", "UpdatePromoCodeEventHandler")]
    [Trait("EventHandler", "Success")]
    public void Handle_PromoCodeObjectFilled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        object parameter = null;
        var updatePromoCode = Create_PromoCode_Object_OK(dpTest);
        var promocode = dpTest.GetDomainEventObject<Domain.Aggregates.PromoCode.PromoCode>(updatePromoCode);
        var repositoryMock = new Mock<IPromoCodeRepository>();
        repositoryMock.Setup((o) => o.Update(promocode)).Returns(true).Callback(() =>
        {
            parameter = promocode;
        });
        var repository = repositoryMock.Object;
        var stateMock = new Mock<IPromoCodeState>();
        stateMock.SetupGet((o) => o.PromoCode).Returns(repository);
        var state = stateMock.Object;
        var updatePromoCodeEventHandler = new Application.EventHandlers.PromoCode.UpdatePromoCodeEventHandler(state, dpTest.MockDp<IPromoCodeState>(state));
        //Act
        var result = updatePromoCodeEventHandler.Handle(updatePromoCode);
        //Assert
        Assert.Equal(parameter, promocode);
        Assert.Equal(result, true);
    }
}