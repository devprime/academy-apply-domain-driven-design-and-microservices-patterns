namespace Core.Tests;
public class CustomerTest
{
    public static Guid FixedID = new Guid("38ed1c93-0afc-46a2-b030-c2f13b0d4a3c");

#region fixtures
    public static Domain.Aggregates.Customer.Customer Create_Customer_Required_Properties_OK(DpTest dpTest)
    {
        var customer = new Domain.Aggregates.Customer.Customer();
        dpTest.MockDpDomain(customer);
        dpTest.Set<Guid>(customer, "ID", FixedID);
        dpTest.Set<string>(customer, "Email", Faker.Lorem.Sentence(1));
        dpTest.Set<string>(customer, "Name", Faker.Lorem.Sentence(1));
        return customer;
    }
    public static Domain.Aggregates.Customer.Customer Create_Customer_With_Email_Required_Property_Missing(DpTest dpTest)
    {
        var customer = new Domain.Aggregates.Customer.Customer();
        dpTest.MockDpDomain(customer);
        dpTest.Set<Guid>(customer, "ID", FixedID);
        dpTest.Set<string>(customer, "Name", Faker.Lorem.Sentence(1));
        return customer;
    }
    public static Domain.Aggregates.Customer.Customer Create_Customer_With_Name_Required_Property_Missing(DpTest dpTest)
    {
        var customer = new Domain.Aggregates.Customer.Customer();
        dpTest.MockDpDomain(customer);
        dpTest.Set<Guid>(customer, "ID", FixedID);
        dpTest.Set<string>(customer, "Email", Faker.Lorem.Sentence(1));
        return customer;
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
        var customer = Create_Customer_Required_Properties_OK(dpTest);
        dpTest.MockDpProcessEvent<bool>(customer, "CreateCustomer", true);
        dpTest.MockDpProcessEvent(customer, "CustomerCreated");
        //Act
        customer.Add();
        //Assert
        var domainevents = dpTest.GetDomainEvents(customer);
        Assert.True(domainevents[0] is CreateCustomer);
        Assert.NotEqual(customer.ID, Guid.Empty);
        Assert.True(customer.IsNew);
        Assert.True(customer.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Add")]
    [Trait("Aggregate", "Fail")]
    public void Add_Email_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var customer = Create_Customer_With_Email_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(customer.Add);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("Email is required", i));
        Assert.False(customer.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Add")]
    [Trait("Aggregate", "Fail")]
    public void Add_Name_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var customer = Create_Customer_With_Name_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(customer.Add);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("Name is required", i));
        Assert.False(customer.Dp.Notifications.IsValid);
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
        var customer = Create_Customer_Required_Properties_OK(dpTest);
        dpTest.MockDpProcessEvent<bool>(customer, "UpdateCustomer", true);
        dpTest.MockDpProcessEvent(customer, "CustomerUpdated");
        //Act
        customer.Update();
        //Assert
        var domainevents = dpTest.GetDomainEvents(customer);
        Assert.True(domainevents[0] is UpdateCustomer);
        Assert.NotEqual(customer.ID, Guid.Empty);
        Assert.True(customer.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Update")]
    [Trait("Aggregate", "Fail")]
    public void Update_Email_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var customer = Create_Customer_With_Email_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(customer.Update);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("Email is required", i));
        Assert.False(customer.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Update")]
    [Trait("Aggregate", "Fail")]
    public void Update_Name_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var customer = Create_Customer_With_Name_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(customer.Update);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("Name is required", i));
        Assert.False(customer.Dp.Notifications.IsValid);
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
        var customer = Create_Customer_Required_Properties_OK(dpTest);
        dpTest.MockDpProcessEvent<bool>(customer, "DeleteCustomer", true);
        dpTest.MockDpProcessEvent(customer, "CustomerDeleted");
        //Act
        customer.Delete();
        //Assert
        var domainevents = dpTest.GetDomainEvents(customer);
        Assert.True(domainevents[0] is DeleteCustomer);
    }

#endregion delete
}