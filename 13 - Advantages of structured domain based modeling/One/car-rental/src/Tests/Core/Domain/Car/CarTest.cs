namespace Core.Tests;
public class CarTest
{
    public static Guid FixedID = new Guid("c4067922-f215-4bfe-a59c-0adba042a738");

#region fixtures
    public static Domain.Aggregates.Car.Car Create_Car_Required_Properties_OK(DpTest dpTest)
    {
        var car = new Domain.Aggregates.Car.Car();
        dpTest.MockDpDomain(car);
        dpTest.Set<Guid>(car, "ID", FixedID);
        dpTest.Set<string>(car, "Model", Faker.Lorem.Sentence(1));
        dpTest.Set<string>(car, "LicensePlate", Faker.Lorem.Sentence(1));
        return car;
    }
    public static Domain.Aggregates.Car.Car Create_Car_With_Model_Required_Property_Missing(DpTest dpTest)
    {
        var car = new Domain.Aggregates.Car.Car();
        dpTest.MockDpDomain(car);
        dpTest.Set<Guid>(car, "ID", FixedID);
        dpTest.Set<string>(car, "LicensePlate", Faker.Lorem.Sentence(1));
        return car;
    }
    public static Domain.Aggregates.Car.Car Create_Car_With_LicensePlate_Required_Property_Missing(DpTest dpTest)
    {
        var car = new Domain.Aggregates.Car.Car();
        dpTest.MockDpDomain(car);
        dpTest.Set<Guid>(car, "ID", FixedID);
        dpTest.Set<string>(car, "Model", Faker.Lorem.Sentence(1));
        return car;
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
        var car = Create_Car_Required_Properties_OK(dpTest);
        dpTest.MockDpProcessEvent<bool>(car, "CreateCar", true);
        dpTest.MockDpProcessEvent(car, "CarCreated");
        //Act
        car.Add();
        //Assert
        var domainevents = dpTest.GetDomainEvents(car);
        Assert.True(domainevents[0] is CreateCar);
        Assert.NotEqual(car.ID, Guid.Empty);
        Assert.True(car.IsNew);
        Assert.True(car.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Add")]
    [Trait("Aggregate", "Fail")]
    public void Add_Model_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var car = Create_Car_With_Model_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(car.Add);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("Model is required", i));
        Assert.False(car.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Add")]
    [Trait("Aggregate", "Fail")]
    public void Add_LicensePlate_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var car = Create_Car_With_LicensePlate_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(car.Add);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("LicensePlate is required", i));
        Assert.False(car.Dp.Notifications.IsValid);
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
        var car = Create_Car_Required_Properties_OK(dpTest);
        dpTest.MockDpProcessEvent<bool>(car, "UpdateCar", true);
        dpTest.MockDpProcessEvent(car, "CarUpdated");
        //Act
        car.Update();
        //Assert
        var domainevents = dpTest.GetDomainEvents(car);
        Assert.True(domainevents[0] is UpdateCar);
        Assert.NotEqual(car.ID, Guid.Empty);
        Assert.True(car.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Update")]
    [Trait("Aggregate", "Fail")]
    public void Update_Model_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var car = Create_Car_With_Model_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(car.Update);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("Model is required", i));
        Assert.False(car.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Update")]
    [Trait("Aggregate", "Fail")]
    public void Update_LicensePlate_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var car = Create_Car_With_LicensePlate_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(car.Update);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("LicensePlate is required", i));
        Assert.False(car.Dp.Notifications.IsValid);
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
        var car = Create_Car_Required_Properties_OK(dpTest);
        dpTest.MockDpProcessEvent<bool>(car, "DeleteCar", true);
        dpTest.MockDpProcessEvent(car, "CarDeleted");
        //Act
        car.Delete();
        //Assert
        var domainevents = dpTest.GetDomainEvents(car);
        Assert.True(domainevents[0] is DeleteCar);
    }

#endregion delete
}