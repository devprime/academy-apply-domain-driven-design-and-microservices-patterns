namespace Core.Tests;
public class RentTest
{
    public static Guid FixedID = new Guid("a35d9675-06ba-49b3-89dd-0d356298386c");

#region fixtures
    public static Domain.Aggregates.Rent.Rent Create_Rent_Required_Properties_OK(DpTest dpTest)
    {
        var rent = new Domain.Aggregates.Rent.Rent();
        dpTest.MockDpDomain(rent);
        dpTest.Set<Guid>(rent, "ID", FixedID);
        dpTest.Set<string>(rent, "LicensePlate", Faker.Lorem.Sentence(1));
        dpTest.Set<string>(rent, "TaxID", Faker.Lorem.Sentence(1));
        dpTest.Set<DateTime>(rent, "Start", DateTime.Now);
        dpTest.Set<DateTime>(rent, "End", DateTime.Now);
        return rent;
    }
    public static Domain.Aggregates.Rent.Rent Create_Rent_With_LicensePlate_Required_Property_Missing(DpTest dpTest)
    {
        var rent = new Domain.Aggregates.Rent.Rent();
        dpTest.MockDpDomain(rent);
        dpTest.Set<Guid>(rent, "ID", FixedID);
        dpTest.Set<string>(rent, "TaxID", Faker.Lorem.Sentence(1));
        dpTest.Set<DateTime>(rent, "Start", DateTime.Now);
        dpTest.Set<DateTime>(rent, "End", DateTime.Now);
        return rent;
    }
    public static Domain.Aggregates.Rent.Rent Create_Rent_With_TaxID_Required_Property_Missing(DpTest dpTest)
    {
        var rent = new Domain.Aggregates.Rent.Rent();
        dpTest.MockDpDomain(rent);
        dpTest.Set<Guid>(rent, "ID", FixedID);
        dpTest.Set<string>(rent, "LicensePlate", Faker.Lorem.Sentence(1));
        dpTest.Set<DateTime>(rent, "Start", DateTime.Now);
        dpTest.Set<DateTime>(rent, "End", DateTime.Now);
        return rent;
    }
    public static Domain.Aggregates.Rent.Rent Create_Rent_With_Start_Required_Property_Missing(DpTest dpTest)
    {
        var rent = new Domain.Aggregates.Rent.Rent();
        dpTest.MockDpDomain(rent);
        dpTest.Set<Guid>(rent, "ID", FixedID);
        dpTest.Set<string>(rent, "LicensePlate", Faker.Lorem.Sentence(1));
        dpTest.Set<string>(rent, "TaxID", Faker.Lorem.Sentence(1));
        dpTest.Set<DateTime>(rent, "End", DateTime.Now);
        return rent;
    }
    public static Domain.Aggregates.Rent.Rent Create_Rent_With_End_Required_Property_Missing(DpTest dpTest)
    {
        var rent = new Domain.Aggregates.Rent.Rent();
        dpTest.MockDpDomain(rent);
        dpTest.Set<Guid>(rent, "ID", FixedID);
        dpTest.Set<string>(rent, "LicensePlate", Faker.Lorem.Sentence(1));
        dpTest.Set<string>(rent, "TaxID", Faker.Lorem.Sentence(1));
        dpTest.Set<DateTime>(rent, "Start", DateTime.Now);
        return rent;
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
        var rent = Create_Rent_Required_Properties_OK(dpTest);
        dpTest.MockDpProcessEvent<bool>(rent, "CreateRent", true);
        dpTest.MockDpProcessEvent(rent, "RentCreated");
        //Act
        rent.Add();
        //Assert
        var domainevents = dpTest.GetDomainEvents(rent);
        Assert.True(domainevents[0] is CreateRent);
        Assert.NotEqual(rent.ID, Guid.Empty);
        Assert.True(rent.IsNew);
        Assert.True(rent.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Add")]
    [Trait("Aggregate", "Fail")]
    public void Add_LicensePlate_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var rent = Create_Rent_With_LicensePlate_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(rent.Add);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("LicensePlate is required", i));
        Assert.False(rent.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Add")]
    [Trait("Aggregate", "Fail")]
    public void Add_TaxID_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var rent = Create_Rent_With_TaxID_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(rent.Add);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("TaxID is required", i));
        Assert.False(rent.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Add")]
    [Trait("Aggregate", "Fail")]
    public void Add_Start_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var rent = Create_Rent_With_Start_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(rent.Add);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("Start is required", i));
        Assert.False(rent.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Add")]
    [Trait("Aggregate", "Fail")]
    public void Add_End_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var rent = Create_Rent_With_End_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(rent.Add);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("End is required", i));
        Assert.False(rent.Dp.Notifications.IsValid);
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
        var rent = Create_Rent_Required_Properties_OK(dpTest);
        dpTest.MockDpProcessEvent<bool>(rent, "UpdateRent", true);
        dpTest.MockDpProcessEvent(rent, "RentUpdated");
        //Act
        rent.Update();
        //Assert
        var domainevents = dpTest.GetDomainEvents(rent);
        Assert.True(domainevents[0] is UpdateRent);
        Assert.NotEqual(rent.ID, Guid.Empty);
        Assert.True(rent.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Update")]
    [Trait("Aggregate", "Fail")]
    public void Update_LicensePlate_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var rent = Create_Rent_With_LicensePlate_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(rent.Update);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("LicensePlate is required", i));
        Assert.False(rent.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Update")]
    [Trait("Aggregate", "Fail")]
    public void Update_TaxID_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var rent = Create_Rent_With_TaxID_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(rent.Update);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("TaxID is required", i));
        Assert.False(rent.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Update")]
    [Trait("Aggregate", "Fail")]
    public void Update_Start_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var rent = Create_Rent_With_Start_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(rent.Update);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("Start is required", i));
        Assert.False(rent.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Update")]
    [Trait("Aggregate", "Fail")]
    public void Update_End_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var rent = Create_Rent_With_End_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(rent.Update);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("End is required", i));
        Assert.False(rent.Dp.Notifications.IsValid);
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
        var rent = Create_Rent_Required_Properties_OK(dpTest);
        dpTest.MockDpProcessEvent<bool>(rent, "DeleteRent", true);
        dpTest.MockDpProcessEvent(rent, "RentDeleted");
        //Act
        rent.Delete();
        //Assert
        var domainevents = dpTest.GetDomainEvents(rent);
        Assert.True(domainevents[0] is DeleteRent);
    }

#endregion delete
}