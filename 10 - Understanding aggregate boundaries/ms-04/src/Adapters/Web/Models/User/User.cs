namespace DevPrime.Web.Models.User;
public class User
{
    public string Name { get; set; }
    public static Application.Services.User.Model.User ToApplication(DevPrime.Web.Models.User.User user)
    {
        if (user is null)
            return new Application.Services.User.Model.User();
        Application.Services.User.Model.User _user = new Application.Services.User.Model.User();
        _user.Name = user.Name;
        return _user;
    }
    public static List<Application.Services.User.Model.User> ToApplication(IList<DevPrime.Web.Models.User.User> userList)
    {
        List<Application.Services.User.Model.User> _userList = new List<Application.Services.User.Model.User>();
        if (userList != null)
        {
            foreach (var user in userList)
            {
                Application.Services.User.Model.User _user = new Application.Services.User.Model.User();
                _user.Name = user.Name;
                _userList.Add(_user);
            }
        }
        return _userList;
    }
    public virtual Application.Services.User.Model.User ToApplication()
    {
        var model = ToApplication(this);
        return model;
    }
}