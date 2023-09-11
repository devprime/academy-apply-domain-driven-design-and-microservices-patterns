namespace Core.Tests;
public class PromoCodeTest
{
    public static Guid FixedID = new Guid("04ca17f1-39b9-4a71-a9c5-45f4b6d16137");

#region fixtures
    public static Domain.Aggregates.PromoCode.PromoCode Create_PromoCode_Required_Properties_OK(DpTest dpTest)
    {
        var promocode = new Domain.Aggregates.PromoCode.PromoCode();
        dpTest.MockDpDomain(promocode);
        dpTest.Set<Guid>(promocode, "ID", FixedID);
        dpTest.Set<string>(promocode, "Code", Faker.Lorem.Sentence(1));
        dpTest.Set<DateTime>(promocode, "ValidUntil", DateTime.Now);
        return promocode;
    }
    public static Domain.Aggregates.PromoCode.PromoCode Create_PromoCode_With_Code_Required_Property_Missing(DpTest dpTest)
    {
        var promocode = new Domain.Aggregates.PromoCode.PromoCode();
        dpTest.MockDpDomain(promocode);
        dpTest.Set<Guid>(promocode, "ID", FixedID);
        dpTest.Set<DateTime>(promocode, "ValidUntil", DateTime.Now);
        return promocode;
    }
    public static Domain.Aggregates.PromoCode.PromoCode Create_PromoCode_With_ValidUntil_Required_Property_Missing(DpTest dpTest)
    {
        var promocode = new Domain.Aggregates.PromoCode.PromoCode();
        dpTest.MockDpDomain(promocode);
        dpTest.Set<Guid>(promocode, "ID", FixedID);
        dpTest.Set<string>(promocode, "Code", Faker.Lorem.Sentence(1));
        return promocode;
    }

#endregion fixtures

#region add
    [Fact]
    [Trait("Aggregate", "Add")]
    [Trait("Aggregate", "Success")]
    public void Add_Required_properties_filled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        var promocode = Create_PromoCode_Required_Properties_OK(dpTest);
        dpTest.MockDpProcessEvent<bool>(promocode, "CreatePromoCode", true);
        dpTest.MockDpProcessEvent(promocode, "PromoCodeCreated");
        //Act
        promocode.Add();
        //Assert
        var domainevents = dpTest.GetDomainEvents(promocode);
        Assert.True(domainevents[0] is CreatePromoCode);
        Assert.True(domainevents[1] is PromoCodeCreated);
        Assert.NotEqual(promocode.ID, Guid.Empty);
        Assert.True(promocode.IsNew);
        Assert.True(promocode.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Add")]
    [Trait("Aggregate", "Fail")]
    public void Add_Code_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var promocode = Create_PromoCode_With_Code_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(promocode.Add);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("Code is required", i));
        Assert.False(promocode.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Add")]
    [Trait("Aggregate", "Fail")]
    public void Add_ValidUntil_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var promocode = Create_PromoCode_With_ValidUntil_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(promocode.Add);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("ValidUntil is required", i));
        Assert.False(promocode.Dp.Notifications.IsValid);
    }

#endregion add

#region update
    [Fact]
    [Trait("Aggregate", "Update")]
    [Trait("Aggregate", "Success")]
    public void Update_Required_properties_filled_Success()
    {
        //Arrange        
        var dpTest = new DpTest();
        var promocode = Create_PromoCode_Required_Properties_OK(dpTest);
        dpTest.MockDpProcessEvent<bool>(promocode, "UpdatePromoCode", true);
        dpTest.MockDpProcessEvent(promocode, "PromoCodeUpdated");
        //Act
        promocode.Update();
        //Assert
        var domainevents = dpTest.GetDomainEvents(promocode);
        Assert.True(domainevents[0] is UpdatePromoCode);
        Assert.True(domainevents[1] is PromoCodeUpdated);
        Assert.NotEqual(promocode.ID, Guid.Empty);
        Assert.True(promocode.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Update")]
    [Trait("Aggregate", "Fail")]
    public void Update_Code_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var promocode = Create_PromoCode_With_Code_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(promocode.Update);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("Code is required", i));
        Assert.False(promocode.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Update")]
    [Trait("Aggregate", "Fail")]
    public void Update_ValidUntil_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var promocode = Create_PromoCode_With_ValidUntil_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(promocode.Update);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("ValidUntil is required", i));
        Assert.False(promocode.Dp.Notifications.IsValid);
    }

#endregion update

#region delete
    [Fact]
    [Trait("Aggregate", "Delete")]
    [Trait("Aggregate", "Success")]
    public void Delete_IDFilled_Success()
    {
        //Arrange
        var dpTest = new DpTest();
        var promocode = Create_PromoCode_Required_Properties_OK(dpTest);
        dpTest.MockDpProcessEvent<bool>(promocode, "DeletePromoCode", true);
        dpTest.MockDpProcessEvent(promocode, "PromoCodeDeleted");
        //Act
        promocode.Delete();
        //Assert
        var domainevents = dpTest.GetDomainEvents(promocode);
        Assert.True(domainevents[0] is DeletePromoCode);
        Assert.True(domainevents[1] is PromoCodeDeleted);
    }

#endregion delete
}