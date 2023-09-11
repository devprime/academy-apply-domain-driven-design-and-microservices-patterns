namespace DevPrime.State.Repositories.UserLicense;
public class UserLicenseRepository : RepositoryBase, IUserLicenseRepository
{
    public UserLicenseRepository(IDpState dp) : base(dp)
    {
        ConnectionAlias = "State1";
    }

#region Write
    public bool Add(Domain.Aggregates.UserLicense.UserLicense userLicense)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var _userLicense = ToState(userLicense);
            state.UserLicense.InsertOne(_userLicense);
            return true;
        });
        if (result is null)
            return false;
        return result;
    }
    public bool Delete(Guid userLicenseID)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            state.UserLicense.DeleteOne(p => p.ID == userLicenseID);
            return true;
        });
        if (result is null)
            return false;
        return result;
    }
    public bool Update(Domain.Aggregates.UserLicense.UserLicense userLicense)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var _userLicense = ToState(userLicense);
            _userLicense._Id = state.UserLicense.Find(p => p.ID == userLicense.ID).FirstOrDefault()._Id;
            state.UserLicense.ReplaceOne(p => p.ID == userLicense.ID, _userLicense);
            return true;
        });
        if (result is null)
            return false;
        return result;
    }

#endregion Write

#region Read
    public Domain.Aggregates.UserLicense.UserLicense Get(Guid userLicenseID)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var userLicense = state.UserLicense.Find(p => p.ID == userLicenseID).FirstOrDefault();
            var _userLicense = ToDomain(userLicense);
            return _userLicense;
        });
    }
    public List<Domain.Aggregates.UserLicense.UserLicense> GetAll(int? limit, int? offset, string ordering, string sort, string filter)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            List<Model.UserLicense> userLicense = null;
            if (sort?.ToLower() == "desc")
            {
                var result = state.UserLicense.Find(GetFilter(filter)).SortByDescending(GetOrdering(ordering));
                if (limit != null && offset != null)
                    userLicense = result.Skip((offset - 1) * limit).Limit(limit).ToList();
                else
                    userLicense = result.ToList();
            }
            else if (sort?.ToLower() == "asc")
            {
                var result = state.UserLicense.Find(GetFilter(filter)).SortBy(GetOrdering(ordering));
                if (limit != null && offset != null)
                    userLicense = result.Skip((offset - 1) * limit).Limit(limit).ToList();
                else
                    userLicense = result.ToList();
            }
            else
            {
                var result = state.UserLicense.Find(GetFilter(filter));
                if (limit != null && offset != null)
                    userLicense = result.Skip((offset - 1) * limit).Limit(limit).ToList();
                else
                    userLicense = result.ToList();
            }
            var _userLicense = ToDomain(userLicense);
            return _userLicense;
        });
    }
    private Expression<Func<Model.UserLicense, object>> GetOrdering(string field)
    {
        Expression<Func<Model.UserLicense, object>> exp = p => p.ID;
        if (!string.IsNullOrWhiteSpace(field))
        {
            if (field.ToLower() == "userid")
                exp = p => p.UserID;
            else if (field.ToLower() == "licenseid")
                exp = p => p.LicenseID;
            else
                exp = p => p.ID;
        }
        return exp;
    }
    private FilterDefinition<Model.UserLicense> GetFilter(string filter)
    {
        var builder = Builders<Model.UserLicense>.Filter;
        FilterDefinition<Model.UserLicense> exp;
        Guid? UserID = null;
        Guid? LicenseID = null;
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
                        if (field.ToLower() == "userid")
                            UserID = new Guid(value);
                        else if (field.ToLower() == "licenseid")
                            LicenseID = new Guid(value);
                    }
                }
            }
        }
        var bfilter = builder.Empty;
        if (UserID != null)
        {
            var UserIDFilter = builder.Eq(x => x.UserID, UserID);
            bfilter &= UserIDFilter;
        }
        if (LicenseID != null)
        {
            var LicenseIDFilter = builder.Eq(x => x.LicenseID, LicenseID);
            bfilter &= LicenseIDFilter;
        }
        exp = bfilter;
        return exp;
    }
    public bool Exists(Guid userLicenseID)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var userLicense = state.UserLicense.Find(x => x.ID == userLicenseID).Project<Model.UserLicense>("{ ID: 1 }").FirstOrDefault();
            return (userLicenseID == userLicense?.ID);
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
            var total = state.UserLicense.Find(GetFilter(filter)).CountDocuments();
            return total;
        });
    }

#endregion Read

#region mappers
    public static DevPrime.State.Repositories.UserLicense.Model.UserLicense ToState(Domain.Aggregates.UserLicense.UserLicense userLicense)
    {
        if (userLicense is null)
            return new DevPrime.State.Repositories.UserLicense.Model.UserLicense();
        DevPrime.State.Repositories.UserLicense.Model.UserLicense _userLicense = new DevPrime.State.Repositories.UserLicense.Model.UserLicense();
        _userLicense.ID = userLicense.ID;
        _userLicense.UserID = userLicense.UserID;
        _userLicense.LicenseID = userLicense.LicenseID;
        return _userLicense;
    }
    public static Domain.Aggregates.UserLicense.UserLicense ToDomain(DevPrime.State.Repositories.UserLicense.Model.UserLicense userLicense)
    {
        if (userLicense is null)
            return new Domain.Aggregates.UserLicense.UserLicense()
            {IsNew = true};
        Domain.Aggregates.UserLicense.UserLicense _userLicense = new Domain.Aggregates.UserLicense.UserLicense(userLicense.ID, userLicense.UserID, userLicense.LicenseID);
        return _userLicense;
    }
    public static List<Domain.Aggregates.UserLicense.UserLicense> ToDomain(IList<DevPrime.State.Repositories.UserLicense.Model.UserLicense> userLicenseList)
    {
        List<Domain.Aggregates.UserLicense.UserLicense> _userLicenseList = new List<Domain.Aggregates.UserLicense.UserLicense>();
        if (userLicenseList != null)
        {
            foreach (var userLicense in userLicenseList)
            {
                Domain.Aggregates.UserLicense.UserLicense _userLicense = new Domain.Aggregates.UserLicense.UserLicense(userLicense.ID, userLicense.UserID, userLicense.LicenseID);
                _userLicenseList.Add(_userLicense);
            }
        }
        return _userLicenseList;
    }

#endregion mappers
}