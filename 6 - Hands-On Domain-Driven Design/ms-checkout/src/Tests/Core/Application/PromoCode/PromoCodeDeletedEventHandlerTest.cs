namespace Core.Tests;
public class PromoCodeDeletedEventHandlerTest
{
    public Dictionary<string, string> CustomSettings()
    {
        var settings = new Dictionary<string, string>();
        settings.Add("stream.promocodeevents", "promocodeevents");
        return settings;
    }
    private PromoCodeDeletedEventDTO SetEventData(Domain.Aggregates.PromoCode.PromoCode promocode)
    {
        return new PromoCodeDeletedEventDTO()
        {ID = promocode.ID, Code = promocode.Code, PercentageDiscount = promocode.PercentageDiscount, Active = promocode.Active, ValidUntil = promocode.ValidUntil};
    }
    public PromoCodeDeleted Create_PromoCode_Object_OK(DpTest dpTest)
    {
        var promocode = PromoCodeTest.Create_PromoCode_Required_Properties_OK(dpTest);
        var promocodeDeleted = new PromoCodeDeleted();
        dpTest.SetDomainEventObject(promocodeDeleted, promocode);
        return promocodeDeleted;
    }
    [Fact]
    [Trait("EventHandler", "PromoCodeDeletedEventHandler")]
    [Trait("EventHandler", "Success")]
    public void Handle_PromoCodeObjectFilled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        var settings = CustomSettings();
        var promocodeDeleted = Create_PromoCode_Object_OK(dpTest);
        var promocode = dpTest.GetDomainEventObject<Domain.Aggregates.PromoCode.PromoCode>(promocodeDeleted);
        var promocodeDeletedEventHandler = new Application.EventHandlers.PromoCode.PromoCodeDeletedEventHandler(null, dpTest.MockDp<IPromoCodeState>(null));
        dpTest.SetupSettings(promocodeDeletedEventHandler.Dp, settings);
        dpTest.SetupStream(promocodeDeletedEventHandler.Dp);
        //Act
        var result = promocodeDeletedEventHandler.Handle(promocodeDeleted);
        //Assert
        var sentEvents = dpTest.GetSentEvents(promocodeDeletedEventHandler.Dp);
        var promocodeDeletedEventDTO = SetEventData(promocode);
        Assert.Equal(sentEvents[0].Destination, settings["stream.promocodeevents"]);
        Assert.Equal("PromoCodeDeleted", sentEvents[0].EventName);
        Assert.Equivalent(sentEvents[0].EventData, promocodeDeletedEventDTO);
        Assert.Equal(result, true);
    }
}