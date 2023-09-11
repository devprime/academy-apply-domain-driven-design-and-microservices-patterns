namespace Core.Tests;
public class PromoCodeCreatedEventHandlerTest
{
    public Dictionary<string, string> CustomSettings()
    {
        var settings = new Dictionary<string, string>();
        settings.Add("stream.promocodeevents", "promocodeevents");
        return settings;
    }
    private PromoCodeCreatedEventDTO SetEventData(Domain.Aggregates.PromoCode.PromoCode promocode)
    {
        return new PromoCodeCreatedEventDTO()
        {ID = promocode.ID, Code = promocode.Code, PercentageDiscount = promocode.PercentageDiscount, Active = promocode.Active, ValidUntil = promocode.ValidUntil};
    }
    public PromoCodeCreated Create_PromoCode_Object_OK(DpTest dpTest)
    {
        var promocode = PromoCodeTest.Create_PromoCode_Required_Properties_OK(dpTest);
        var promocodeCreated = new PromoCodeCreated();
        dpTest.SetDomainEventObject(promocodeCreated, promocode);
        return promocodeCreated;
    }
    [Fact]
    [Trait("EventHandler", "PromoCodeCreatedEventHandler")]
    [Trait("EventHandler", "Success")]
    public void Handle_PromoCodeObjectFilled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        var settings = CustomSettings();
        var promocodeCreated = Create_PromoCode_Object_OK(dpTest);
        var promocode = dpTest.GetDomainEventObject<Domain.Aggregates.PromoCode.PromoCode>(promocodeCreated);
        var promocodeCreatedEventHandler = new Application.EventHandlers.PromoCode.PromoCodeCreatedEventHandler(null, dpTest.MockDp<IPromoCodeState>(null));
        dpTest.SetupSettings(promocodeCreatedEventHandler.Dp, settings);
        dpTest.SetupStream(promocodeCreatedEventHandler.Dp);
        //Act
        var result = promocodeCreatedEventHandler.Handle(promocodeCreated);
        //Assert
        var sentEvents = dpTest.GetSentEvents(promocodeCreatedEventHandler.Dp);
        var promocodeCreatedEventDTO = SetEventData(promocode);
        Assert.Equal(sentEvents[0].Destination, settings["stream.promocodeevents"]);
        Assert.Equal("PromoCodeCreated", sentEvents[0].EventName);
        Assert.Equivalent(sentEvents[0].EventData, promocodeCreatedEventDTO);
        Assert.Equal(result, true);
    }
}