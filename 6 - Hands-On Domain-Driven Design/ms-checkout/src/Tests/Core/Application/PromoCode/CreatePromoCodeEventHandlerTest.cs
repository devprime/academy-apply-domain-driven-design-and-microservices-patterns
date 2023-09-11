namespace Core.Tests;
public class CreatePromoCodeEventHandlerTest
{
    public CreatePromoCode Create_PromoCode_Object_OK(DpTest dpTest)
    {
        var promocode = PromoCodeTest.Create_PromoCode_Required_Properties_OK(dpTest);
        var createPromoCode = new CreatePromoCode();
        dpTest.SetDomainEventObject(createPromoCode, promocode);
        return createPromoCode;
    }
    [Fact]
    [Trait("EventHandler", "CreatePromoCodeEventHandler")]
    [Trait("EventHandler", "Success")]
    public void Handle_PromoCodeObjectFilled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        object parameter = null;
        var createPromoCode = Create_PromoCode_Object_OK(dpTest);
        var promocode = dpTest.GetDomainEventObject<Domain.Aggregates.PromoCode.PromoCode>(createPromoCode);
        var repositoryMock = new Mock<IPromoCodeRepository>();
        repositoryMock.Setup((o) => o.Add(promocode)).Returns(true).Callback(() =>
        {
            parameter = promocode;
        });
        var repository = repositoryMock.Object;
        var stateMock = new Mock<IPromoCodeState>();
        stateMock.SetupGet((o) => o.PromoCode).Returns(repository);
        var state = stateMock.Object;
        var createPromoCodeEventHandler = new Application.EventHandlers.PromoCode.CreatePromoCodeEventHandler(state, dpTest.MockDp<IPromoCodeState>(state));
        //Act
        var result = createPromoCodeEventHandler.Handle(createPromoCode);
        //Assert
        Assert.Equal(parameter, promocode);
        Assert.Equal(result, true);
    }
}