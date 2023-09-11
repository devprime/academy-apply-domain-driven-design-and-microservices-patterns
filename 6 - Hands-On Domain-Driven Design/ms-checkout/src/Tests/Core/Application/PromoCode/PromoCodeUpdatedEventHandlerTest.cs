namespace Core.Tests;
public class PromoCodeUpdatedEventHandlerTest
{
    public Dictionary<string, string> CustomSettings()
    {
        var settings = new Dictionary<string, string>();
        settings.Add("stream.promocodeevents", "promocodeevents");
        return settings;
    }
    private PromoCodeUpdatedEventDTO SetEventData(Domain.Aggregates.PromoCode.PromoCode promocode)
    {
        return new PromoCodeUpdatedEventDTO()
        {ID = promocode.ID, Code = promocode.Code, PercentageDiscount = promocode.PercentageDiscount, Active = promocode.Active, ValidUntil = promocode.ValidUntil};
    }
    public PromoCodeUpdated Create_PromoCode_Object_OK(DpTest dpTest)
    {
        var promocode = PromoCodeTest.Create_PromoCode_Required_Properties_OK(dpTest);
        var promocodeUpdated = new PromoCodeUpdated();
        dpTest.SetDomainEventObject(promocodeUpdated, promocode);
        return promocodeUpdated;
    }
    [Fact]
    [Trait("EventHandler", "PromoCodeUpdatedEventHandler")]
    [Trait("EventHandler", "Success")]
    public void Handle_PromoCodeObjectFilled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        var settings = CustomSettings();
        var promocodeUpdated = Create_PromoCode_Object_OK(dpTest);
        var promocode = dpTest.GetDomainEventObject<Domain.Aggregates.PromoCode.PromoCode>(promocodeUpdated);
        var promocodeUpdatedEventHandler = new Application.EventHandlers.PromoCode.PromoCodeUpdatedEventHandler(null, dpTest.MockDp<IPromoCodeState>(null));
        dpTest.SetupSettings(promocodeUpdatedEventHandler.Dp, settings);
        dpTest.SetupStream(promocodeUpdatedEventHandler.Dp);
        //Act
        var result = promocodeUpdatedEventHandler.Handle(promocodeUpdated);
        //Assert
        var sentEvents = dpTest.GetSentEvents(promocodeUpdatedEventHandler.Dp);
        var promocodeUpdatedEventDTO = SetEventData(promocode);
        Assert.Equal(sentEvents[0].Destination, settings["stream.promocodeevents"]);
        Assert.Equal("PromoCodeUpdated", sentEvents[0].EventName);
        Assert.Equivalent(sentEvents[0].EventData, promocodeUpdatedEventDTO);
        Assert.Equal(result, true);
    }
}