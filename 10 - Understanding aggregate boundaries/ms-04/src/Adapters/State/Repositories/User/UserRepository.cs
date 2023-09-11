namespace DevPrime.State.Repositories.User;
public class UserRepository : RepositoryBase, IUserRepository
{
    public UserRepository(IDpState dp) : base(dp)
    {
        ConnectionAlias = "State1";
    }

#region Write
    public bool Add(Domain.Aggregates.User.User user)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var _user = ToState(user);
            state.User.InsertOne(_user);
            return true;
        });
        if (result is null)
            return false;
        return result;
    }
    public bool Delete(Guid userID)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            state.User.DeleteOne(p => p.ID == userID);
            return true;
        });
        if (result is null)
            return false;
        return result;
    }
    public bool Update(Domain.Aggregates.User.User user)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var _user = ToState(user);
            _user._Id = state.User.Find(p => p.ID == user.ID).FirstOrDefault()._Id;
            state.User.ReplaceOne(p => p.ID == user.ID, _user);
            return true;
        });
        if (result is null)
            return false;
        return result;
    }

#endregion Write

#region Read
    public Domain.Aggregates.User.User Get(Guid userID)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var user = state.User.Find(p => p.ID == userID).FirstOrDefault();
            var _user = ToDomain(user);
            return _user;
        });
    }
    public List<Domain.Aggregates.User.User> GetAll(int? limit, int? offset, string ordering, string sort, string filter)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            List<Model.User> user = null;
            if (sort?.ToLower() == "desc")
            {
                var result = state.User.Find(GetFilter(filter)).SortByDescending(GetOrdering(ordering));
                if (limit != null && offset != null)
                    user = result.Skip((offset - 1) * limit).Limit(limit).ToList();
                else
                    user = result.ToList();
            }
            else if (sort?.ToLower() == "asc")
            {
                var result = state.User.Find(GetFilter(filter)).SortBy(GetOrdering(ordering));
                if (limit != null && offset != null)
                    user = result.Skip((offset - 1) * limit).Limit(limit).ToList();
                else
                    user = result.ToList();
            }
            else
            {
                var result = state.User.Find(GetFilter(filter));
                if (limit != null && offset != null)
                    user = result.Skip((offset - 1) * limit).Limit(limit).ToList();
                else
                    user = result.ToList();
            }
            var _user = ToDomain(user);
            return _user;
        });
    }
    private Expression<Func<Model.User, object>> GetOrdering(string field)
    {
        Expression<Func<Model.User, object>> exp = p => p.ID;
        if (!string.IsNullOrWhiteSpace(field))
        {
            if (field.ToLower() == "name")
                exp = p => p.Name;
            else
                exp = p => p.ID;
        }
        return exp;
    }
    private FilterDefinition<Model.User> GetFilter(string filter)
    {
        var builder = Builders<Model.User>.Filter;
        FilterDefinition<Model.User> exp;
        string Name = string.Empty;
        if (!string.IsNullOrWhiteSpace(filter))
        {
            var conditions = filter.Split(",");
            if (conditions.Count() >= 1)
            {
                foreach (var condition in conditions)
                {
                    var slice = condition?.Split("=");
                    if (slice.Length > 1)
                    {
                        var field = slice[0];
                        var value = slice[1];
                        if (field.ToLower() == "name")
                            Name = value;
                    }
                }
            }
        }
        var bfilter = builder.Empty;
        if (!string.IsNullOrWhiteSpace(Name))
        {
            var NameFilter = builder.Eq(x => x.Name, Name);
            bfilter &= NameFilter;
        }
        exp = bfilter;
        return exp;
    }
    public bool Exists(Guid userID)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var user = state.User.Find(x => x.ID == userID).Project<Model.User>("{ ID: 1 }").FirstOrDefault();
            return (userID == user?.ID);
        });
        if (result is null)
            return false;
        return result;
    }
    public long Total(string filter)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var total = state.User.Find(GetFilter(filter)).CountDocuments();
            return total;
        });
    }

#endregion Read

#region mappers
    public static DevPrime.State.Repositories.User.Model.User ToState(Domain.Aggregates.User.User user)
    {
        if (user is null)
            return new DevPrime.State.Repositories.User.Model.User();
        DevPrime.State.Repositories.User.Model.User _user = new DevPrime.State.Repositories.User.Model.User();
        _user.ID = user.ID;
        _user.Name = user.Name;
        return _user;
    }
    public static Domain.Aggregates.User.User ToDomain(DevPrime.State.Repositories.User.Model.User user)
    {
        if (user is null)
            return new Domain.Aggregates.User.User()
            {IsNew = true};
        Domain.Aggregates.User.User _user = new Domain.Aggregates.User.User(user.ID, user.Name);
        return _user;
    }
    public static List<Domain.Aggregates.User.User> ToDomain(IList<DevPrime.State.Repositories.User.Model.User> userList)
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

#endregion mappers
}