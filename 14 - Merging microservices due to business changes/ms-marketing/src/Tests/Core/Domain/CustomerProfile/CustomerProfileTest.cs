namespace Core.Tests;
public class CustomerProfileTest
{
    public static Guid FixedID = new Guid("77dd9bf5-2b0c-450a-b668-8f0891dc0a56");
    public static Guid CustomerIDFixedID = new Guid("62c63f78-79cd-41a2-a939-caac47c6abc1");

#region fixtures
    public static Domain.Aggregates.CustomerProfile.CustomerProfile Create_CustomerProfile_Required_Properties_OK(DpTest dpTest)
    {
        var customerprofile = new Domain.Aggregates.CustomerProfile.CustomerProfile();
        dpTest.MockDpDomain(customerprofile);
        dpTest.Set<Guid>(customerprofile, "ID", FixedID);
        dpTest.Set<Guid>(customerprofile, "CustomerID", CustomerIDFixedID);
        dpTest.Set<string>(customerprofile, "Email", Faker.Lorem.Sentence(1));
        dpTest.Set<string>(customerprofile, "Name", Faker.Lorem.Sentence(1));
        dpTest.Set<string>(customerprofile, "Photo", Faker.Lorem.Sentence(1));
        dpTest.Set<DateTime>(customerprofile, "BirthDate", DateTime.Now);
        return customerprofile;
    }
    public static Domain.Aggregates.CustomerProfile.CustomerProfile Create_CustomerProfile_With_Email_Required_Property_Missing(DpTest dpTest)
    {
        var customerprofile = new Domain.Aggregates.CustomerProfile.CustomerProfile();
        dpTest.MockDpDomain(customerprofile);
        dpTest.Set<Guid>(customerprofile, "ID", FixedID);
        dpTest.Set<Guid>(customerprofile, "CustomerID", CustomerIDFixedID);
        dpTest.Set<string>(customerprofile, "Name", Faker.Lorem.Sentence(1));
        dpTest.Set<string>(customerprofile, "Photo", Faker.Lorem.Sentence(1));
        dpTest.Set<DateTime>(customerprofile, "BirthDate", DateTime.Now);
        return customerprofile;
    }
    public static Domain.Aggregates.CustomerProfile.CustomerProfile Create_CustomerProfile_With_Name_Required_Property_Missing(DpTest dpTest)
    {
        var customerprofile = new Domain.Aggregates.CustomerProfile.CustomerProfile();
        dpTest.MockDpDomain(customerprofile);
        dpTest.Set<Guid>(customerprofile, "ID", FixedID);
        dpTest.Set<Guid>(customerprofile, "CustomerID", CustomerIDFixedID);
        dpTest.Set<string>(customerprofile, "Email", Faker.Lorem.Sentence(1));
        dpTest.Set<string>(customerprofile, "Photo", Faker.Lorem.Sentence(1));
        dpTest.Set<DateTime>(customerprofile, "BirthDate", DateTime.Now);
        return customerprofile;
    }
    public static Domain.Aggregates.CustomerProfile.CustomerProfile Create_CustomerProfile_With_Photo_Required_Property_Missing(DpTest dpTest)
    {
        var customerprofile = new Domain.Aggregates.CustomerProfile.CustomerProfile();
        dpTest.MockDpDomain(customerprofile);
        dpTest.Set<Guid>(customerprofile, "ID", FixedID);
        dpTest.Set<Guid>(customerprofile, "CustomerID", CustomerIDFixedID);
        dpTest.Set<string>(customerprofile, "Email", Faker.Lorem.Sentence(1));
        dpTest.Set<string>(customerprofile, "Name", Faker.Lorem.Sentence(1));
        dpTest.Set<DateTime>(customerprofile, "BirthDate", DateTime.Now);
        return customerprofile;
    }
    public static Domain.Aggregates.CustomerProfile.CustomerProfile Create_CustomerProfile_With_BirthDate_Required_Property_Missing(DpTest dpTest)
    {
        var customerprofile = new Domain.Aggregates.CustomerProfile.CustomerProfile();
        dpTest.MockDpDomain(customerprofile);
        dpTest.Set<Guid>(customerprofile, "ID", FixedID);
        dpTest.Set<Guid>(customerprofile, "CustomerID", CustomerIDFixedID);
        dpTest.Set<string>(customerprofile, "Email", Faker.Lorem.Sentence(1));
        dpTest.Set<string>(customerprofile, "Name", Faker.Lorem.Sentence(1));
        dpTest.Set<string>(customerprofile, "Photo", Faker.Lorem.Sentence(1));
        return customerprofile;
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
        var customerprofile = Create_CustomerProfile_Required_Properties_OK(dpTest);
        dpTest.MockDpProcessEvent<bool>(customerprofile, "CreateCustomerProfile", true);
        dpTest.MockDpProcessEvent(customerprofile, "CustomerProfileCreated");
        //Act
        customerprofile.Add();
        //Assert
        var domainevents = dpTest.GetDomainEvents(customerprofile);
        Assert.True(domainevents[0] is CreateCustomerProfile);
        Assert.NotEqual(customerprofile.ID, Guid.Empty);
        Assert.True(customerprofile.IsNew);
        Assert.True(customerprofile.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Add")]
    [Trait("Aggregate", "Fail")]
    public void Add_Email_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var customerprofile = Create_CustomerProfile_With_Email_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(customerprofile.Add);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("Email is required", i));
        Assert.False(customerprofile.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Add")]
    [Trait("Aggregate", "Fail")]
    public void Add_Name_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var customerprofile = Create_CustomerProfile_With_Name_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(customerprofile.Add);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("Name is required", i));
        Assert.False(customerprofile.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Add")]
    [Trait("Aggregate", "Fail")]
    public void Add_Photo_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var customerprofile = Create_CustomerProfile_With_Photo_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(customerprofile.Add);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("Photo is required", i));
        Assert.False(customerprofile.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Add")]
    [Trait("Aggregate", "Fail")]
    public void Add_BirthDate_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var customerprofile = Create_CustomerProfile_With_BirthDate_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(customerprofile.Add);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("BirthDate is required", i));
        Assert.False(customerprofile.Dp.Notifications.IsValid);
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
        var customerprofile = Create_CustomerProfile_Required_Properties_OK(dpTest);
        dpTest.MockDpProcessEvent<bool>(customerprofile, "UpdateCustomerProfile", true);
        dpTest.MockDpProcessEvent(customerprofile, "CustomerProfileUpdated");
        //Act
        customerprofile.Update();
        //Assert
        var domainevents = dpTest.GetDomainEvents(customerprofile);
        Assert.True(domainevents[0] is UpdateCustomerProfile);
        Assert.NotEqual(customerprofile.ID, Guid.Empty);
        Assert.True(customerprofile.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Update")]
    [Trait("Aggregate", "Fail")]
    public void Update_Email_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var customerprofile = Create_CustomerProfile_With_Email_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(customerprofile.Update);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("Email is required", i));
        Assert.False(customerprofile.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Update")]
    [Trait("Aggregate", "Fail")]
    public void Update_Name_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var customerprofile = Create_CustomerProfile_With_Name_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(customerprofile.Update);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("Name is required", i));
        Assert.False(customerprofile.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Update")]
    [Trait("Aggregate", "Fail")]
    public void Update_Photo_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var customerprofile = Create_CustomerProfile_With_Photo_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(customerprofile.Update);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("Photo is required", i));
        Assert.False(customerprofile.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Update")]
    [Trait("Aggregate", "Fail")]
    public void Update_BirthDate_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var customerprofile = Create_CustomerProfile_With_BirthDate_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(customerprofile.Update);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("BirthDate is required", i));
        Assert.False(customerprofile.Dp.Notifications.IsValid);
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
        var customerprofile = Create_CustomerProfile_Required_Properties_OK(dpTest);
        dpTest.MockDpProcessEvent<bool>(customerprofile, "DeleteCustomerProfile", true);
        dpTest.MockDpProcessEvent(customerprofile, "CustomerProfileDeleted");
        //Act
        customerprofile.Delete();
        //Assert
        var domainevents = dpTest.GetDomainEvents(customerprofile);
        Assert.True(domainevents[0] is DeleteCustomerProfile);
    }

#endregion delete
}