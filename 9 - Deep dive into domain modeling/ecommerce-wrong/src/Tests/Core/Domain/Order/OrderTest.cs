namespace Core.Tests;
public class OrderTest
{
    public static Guid FixedID = new Guid("c446a736-6dd0-4309-9ad5-1cf46d74dfb0");

#region fixtures
    public static Domain.Aggregates.Order.Order Create_Order_Required_Properties_OK(DpTest dpTest)
    {
        var order = new Domain.Aggregates.Order.Order();
        dpTest.MockDpDomain(order);
        dpTest.Set<Guid>(order, "ID", FixedID);
        dpTest.Set<string>(order, "CustomerName", Faker.Lorem.Sentence(1));
        dpTest.Set<string>(order, "CustomerEmail", Faker.Lorem.Sentence(1));
        return order;
    }
    public static Domain.Aggregates.Order.Order Create_Order_With_CustomerName_Required_Property_Missing(DpTest dpTest)
    {
        var order = new Domain.Aggregates.Order.Order();
        dpTest.MockDpDomain(order);
        dpTest.Set<Guid>(order, "ID", FixedID);
        dpTest.Set<string>(order, "CustomerEmail", Faker.Lorem.Sentence(1));
        return order;
    }
    public static Domain.Aggregates.Order.Order Create_Order_With_CustomerEmail_Required_Property_Missing(DpTest dpTest)
    {
        var order = new Domain.Aggregates.Order.Order();
        dpTest.MockDpDomain(order);
        dpTest.Set<Guid>(order, "ID", FixedID);
        dpTest.Set<string>(order, "CustomerName", Faker.Lorem.Sentence(1));
        return order;
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
        var order = Create_Order_Required_Properties_OK(dpTest);
        dpTest.MockDpProcessEvent<bool>(order, "CreateOrder", true);
        dpTest.MockDpProcessEvent(order, "OrderCreated");
        //Act
        order.Add();
        //Assert
        var domainevents = dpTest.GetDomainEvents(order);
        Assert.True(domainevents[0] is CreateOrder);
        Assert.NotEqual(order.ID, Guid.Empty);
        Assert.True(order.IsNew);
        Assert.True(order.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Add")]
    [Trait("Aggregate", "Fail")]
    public void Add_CustomerName_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var order = Create_Order_With_CustomerName_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(order.Add);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("CustomerName is required", i));
        Assert.False(order.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Add")]
    [Trait("Aggregate", "Fail")]
    public void Add_CustomerEmail_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var order = Create_Order_With_CustomerEmail_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(order.Add);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("CustomerEmail is required", i));
        Assert.False(order.Dp.Notifications.IsValid);
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
        var order = Create_Order_Required_Properties_OK(dpTest);
        dpTest.MockDpProcessEvent<bool>(order, "UpdateOrder", true);
        dpTest.MockDpProcessEvent(order, "OrderUpdated");
        //Act
        order.Update();
        //Assert
        var domainevents = dpTest.GetDomainEvents(order);
        Assert.True(domainevents[0] is UpdateOrder);
        Assert.NotEqual(order.ID, Guid.Empty);
        Assert.True(order.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Update")]
    [Trait("Aggregate", "Fail")]
    public void Update_CustomerName_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var order = Create_Order_With_CustomerName_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(order.Update);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("CustomerName is required", i));
        Assert.False(order.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Update")]
    [Trait("Aggregate", "Fail")]
    public void Update_CustomerEmail_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var order = Create_Order_With_CustomerEmail_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(order.Update);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("CustomerEmail is required", i));
        Assert.False(order.Dp.Notifications.IsValid);
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
        var order = Create_Order_Required_Properties_OK(dpTest);
        dpTest.MockDpProcessEvent<bool>(order, "DeleteOrder", true);
        dpTest.MockDpProcessEvent(order, "OrderDeleted");
        //Act
        order.Delete();
        //Assert
        var domainevents = dpTest.GetDomainEvents(order);
        Assert.True(domainevents[0] is DeleteOrder);
    }

#endregion delete
}