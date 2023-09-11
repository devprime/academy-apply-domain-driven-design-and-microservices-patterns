namespace Core.Tests;
public class UserTest
{
    public static Guid FixedID = new Guid("0bc633b4-54b7-434c-ac54-173a233f87a9");

#region fixtures
    public static Domain.Aggregates.User.User Create_User_Required_Properties_OK(DpTest dpTest)
    {
        var user = new Domain.Aggregates.User.User();
        dpTest.MockDpDomain(user);
        dpTest.Set<Guid>(user, "ID", FixedID);
        dpTest.Set<string>(user, "Name", Faker.Lorem.Sentence(1));
        return user;
    }
    public static Domain.Aggregates.User.User Create_User_With_Name_Required_Property_Missing(DpTest dpTest)
    {
        var user = new Domain.Aggregates.User.User();
        dpTest.MockDpDomain(user);
        dpTest.Set<Guid>(user, "ID", FixedID);
        return user;
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
        var user = Create_User_Required_Properties_OK(dpTest);
        dpTest.MockDpProcessEvent<bool>(user, "CreateUser", true);
        dpTest.MockDpProcessEvent(user, "UserCreated");
        //Act
        user.Add();
        //Assert
        var domainevents = dpTest.GetDomainEvents(user);
        Assert.True(domainevents[0] is CreateUser);
        Assert.NotEqual(user.ID, Guid.Empty);
        Assert.True(user.IsNew);
        Assert.True(user.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Add")]
    [Trait("Aggregate", "Fail")]
    public void Add_Name_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var user = Create_User_With_Name_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(user.Add);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("Name is required", i));
        Assert.False(user.Dp.Notifications.IsValid);
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
        var user = Create_User_Required_Properties_OK(dpTest);
        dpTest.MockDpProcessEvent<bool>(user, "UpdateUser", true);
        dpTest.MockDpProcessEvent(user, "UserUpdated");
        //Act
        user.Update();
        //Assert
        var domainevents = dpTest.GetDomainEvents(user);
        Assert.True(domainevents[0] is UpdateUser);
        Assert.NotEqual(user.ID, Guid.Empty);
        Assert.True(user.Dp.Notifications.IsValid);
    }
    [Fact]
    [Trait("Aggregate", "Update")]
    [Trait("Aggregate", "Fail")]
    public void Update_Name_Missing_Fail()
    {
        //Arrange
        var dpTest = new DpTest();
        var user = Create_User_With_Name_Required_Property_Missing(dpTest);
        //Act and Assert
        var ex = Assert.Throws<PublicException>(user.Update);
        Assert.Equal("Public exception", ex.ErrorMessage);
        Assert.Collection(ex.Exceptions, i => Assert.Equal("Name is required", i));
        Assert.False(user.Dp.Notifications.IsValid);
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
        var user = Create_User_Required_Properties_OK(dpTest);
        dpTest.MockDpProcessEvent<bool>(user, "DeleteUser", true);
        dpTest.MockDpProcessEvent(user, "UserDeleted");
        //Act
        user.Delete();
        //Assert
        var domainevents = dpTest.GetDomainEvents(user);
        Assert.True(domainevents[0] is DeleteUser);
    }

#endregion delete
}