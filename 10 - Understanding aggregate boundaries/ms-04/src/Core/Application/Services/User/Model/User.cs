namespace Application.Services.User.Model;
public class User
{
    internal int? Limit { get; set; }
    internal int? Offset { get; set; }
    internal string Ordering { get; set; }
    internal string Filter { get; set; }
    internal string Sort { get; set; }
    public User(int? limit, int? offset, string ordering, string sort, string filter)
    {
        Limit = limit;
        Offset = offset;
        Ordering = ordering;
        Filter = filter;
        Sort = sort;
    }
    public Guid ID { get; set; }
    public string Name { get; set; }
    public virtual PagingResult<IList<User>> ToUserList(IList<Domain.Aggregates.User.User> userList, long? total, int? offSet, int? limit)
    {
        var _userList = ToApplication(userList);
        return new PagingResult<IList<User>>(_userList, total, offSet, limit);
    }
    public virtual User ToUser(Domain.Aggregates.User.User user)
    {
        var _user = ToApplication(user);
        return _user;
    }
    public virtual Domain.Aggregates.User.User ToDomain()
    {
        var _user = ToDomain(this);
        return _user;
    }
    public virtual Domain.Aggregates.User.User ToDomain(Guid id)
    {
        var _user = new Domain.Aggregates.User.User();
        _user.ID = id;
        return _user;
    }
    public User()
    {
    }
    public User(Guid id)
    {
        ID = id;
    }
    public static Application.Services.User.Model.User ToApplication(Domain.Aggregates.User.User user)
    {
        if (user is null)
            return new Application.Services.User.Model.User();
        Application.Services.User.Model.User _user = new Application.Services.User.Model.User();
        _user.ID = user.ID;
        _user.Name = user.Name;
        return _user;
    }
    public static List<Application.Services.User.Model.User> ToApplication(IList<Domain.Aggregates.User.User> userList)
    {
        List<Application.Services.User.Model.User> _userList = new List<Application.Services.User.Model.User>();
        if (userList != null)
        {
            foreach (var user in userList)
            {
                Application.Services.User.Model.User _user = new Application.Services.User.Model.User();
                _user.ID = user.ID;
                _user.Name = user.Name;
                _userList.Add(_user);
            }
        }
        return _userList;
    }
    public static Domain.Aggregates.User.User ToDomain(Application.Services.User.Model.User user)
    {
        if (user is null)
            return new Domain.Aggregates.User.User();
        Domain.Aggregates.User.User _user = new Domain.Aggregates.User.User(user.ID, user.Name);
        return _user;
    }
    public static List<Domain.Aggregates.User.User> ToDomain(IList<Application.Services.User.Model.User> userList)
    {
        List<Domain.Aggregates.User.User> _userList = new List<Domain.Aggregates.User.User>();
        if (userList != null)
        {
            foreach (var user in userList)
            {
                Domain.Aggregates.User.User _user = new Domain.Aggregates.User.User(user.ID, user.Name);
                _userList.Add(_user);
            }
        }
        return _userList;
    }
}