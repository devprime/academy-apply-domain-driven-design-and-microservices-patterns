namespace Core.Tests;
public class DeletePromoCodeEventHandlerTest
{
    public DeletePromoCode Create_PromoCode_Object_OK(DpTest dpTest)
    {
        var promocode = PromoCodeTest.Create_PromoCode_Required_Properties_OK(dpTest);
        var deletePromoCode = new DeletePromoCode();
        dpTest.SetDomainEventObject(deletePromoCode, promocode);
        return deletePromoCode;
    }
    [Fact]
    [Trait("EventHandler", "DeletePromoCodeEventHandler")]
    [Trait("EventHandler", "Success")]
    public void Handle_PromoCodeObjectFilled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        object parameter = null;
        var deletePromoCode = Create_PromoCode_Object_OK(dpTest);
        var promocode = dpTest.GetDomainEventObject<Domain.Aggregates.PromoCode.PromoCode>(deletePromoCode);
        var repositoryMock = new Mock<IPromoCodeRepository>();
        repositoryMock.Setup((o) => o.Delete(promocode.ID)).Returns(true).Callback(() =>
        {
            parameter = promocode;
        });
        var repository = repositoryMock.Object;
        var stateMock = new Mock<IPromoCodeState>();
        stateMock.SetupGet((o) => o.PromoCode).Returns(repository);
        var state = stateMock.Object;
        var deletePromoCodeEventHandler = new Application.EventHandlers.PromoCode.DeletePromoCodeEventHandler(state, dpTest.MockDp<IPromoCodeState>(state));
        //Act
        var result = deletePromoCodeEventHandler.Handle(deletePromoCode);
        //Assert
        Assert.Equal(parameter, promocode);
        Assert.Equal(result, true);
    }
}