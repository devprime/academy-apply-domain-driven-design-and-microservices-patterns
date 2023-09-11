namespace DevPrime.State.Repositories.CustomerProfile;
public class CustomerProfileRepository : RepositoryBase, ICustomerProfileRepository
{
    public CustomerProfileRepository(IDpState dp) : base(dp)
    {
        ConnectionAlias = "State1";
    }

#region Write
    public bool Add(Domain.Aggregates.CustomerProfile.CustomerProfile customerProfile)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var _customerProfile = ToState(customerProfile);
            state.CustomerProfile.InsertOne(_customerProfile);
            return true;
        });
        if (result is null)
            return false;
        return result;
    }
    public bool Delete(Guid customerProfileID)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            state.CustomerProfile.DeleteOne(p => p.ID == customerProfileID);
            return true;
        });
        if (result is null)
            return false;
        return result;
    }
    public bool Update(Domain.Aggregates.CustomerProfile.CustomerProfile customerProfile)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var _customerProfile = ToState(customerProfile);
            _customerProfile._Id = state.CustomerProfile.Find(p => p.ID == customerProfile.ID).FirstOrDefault()._Id;
            state.CustomerProfile.ReplaceOne(p => p.ID == customerProfile.ID, _customerProfile);
            return true;
        });
        if (result is null)
            return false;
        return result;
    }

#endregion Write

#region Read
    public Domain.Aggregates.CustomerProfile.CustomerProfile Get(Guid customerProfileID)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var customerProfile = state.CustomerProfile.Find(p => p.ID == customerProfileID).FirstOrDefault();
            var _customerProfile = ToDomain(customerProfile);
            return _customerProfile;
        });
    }
    public List<Domain.Aggregates.CustomerProfile.CustomerProfile> GetAll(int? limit, int? offset, string ordering, string sort, string filter)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            List<Model.CustomerProfile> customerProfile = null;
            if (sort?.ToLower() == "desc")
            {
                var result = state.CustomerProfile.Find(GetFilter(filter)).SortByDescending(GetOrdering(ordering));
                if (limit != null && offset != null)
                    customerProfile = result.Skip((offset - 1) * limit).Limit(limit).ToList();
                else
                    customerProfile = result.ToList();
            }
            else if (sort?.ToLower() == "asc")
            {
                var result = state.CustomerProfile.Find(GetFilter(filter)).SortBy(GetOrdering(ordering));
                if (limit != null && offset != null)
                    customerProfile = result.Skip((offset - 1) * limit).Limit(limit).ToList();
                else
                    customerProfile = result.ToList();
            }
            else
            {
                var result = state.CustomerProfile.Find(GetFilter(filter));
                if (limit != null && offset != null)
                    customerProfile = result.Skip((offset - 1) * limit).Limit(limit).ToList();
                else
                    customerProfile = result.ToList();
            }
            var _customerProfile = ToDomain(customerProfile);
            return _customerProfile;
        });
    }
    private Expression<Func<Model.CustomerProfile, object>> GetOrdering(string field)
    {
        Expression<Func<Model.CustomerProfile, object>> exp = p => p.ID;
        if (!string.IsNullOrWhiteSpace(field))
        {
            if (field.ToLower() == "customerid")
                exp = p => p.CustomerID;
            else if (field.ToLower() == "email")
                exp = p => p.Email;
            else if (field.ToLower() == "name")
                exp = p => p.Name;
            else if (field.ToLower() == "photo")
                exp = p => p.Photo;
            else if (field.ToLower() == "birthdate")
                exp = p => p.BirthDate;
            else if (field.ToLower() == "score")
                exp = p => p.Score;
            else
                exp = p => p.ID;
        }
        return exp;
    }
    private FilterDefinition<Model.CustomerProfile> GetFilter(string filter)
    {
        var builder = Builders<Model.CustomerProfile>.Filter;
        FilterDefinition<Model.CustomerProfile> exp;
        Guid? CustomerID = null;
        string Email = string.Empty;
        string Name = string.Empty;
        string Photo = string.Empty;
        DateTime? BirthDate = null;
        Double? Score = null;
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
                        if (field.ToLower() == "customerid")
                            CustomerID = new Guid(value);
                        else if (field.ToLower() == "email")
                            Email = value;
                        else if (field.ToLower() == "name")
                            Name = value;
                        else if (field.ToLower() == "photo")
                            Photo = value;
                        else if (field.ToLower() == "birthdate")
                            BirthDate = Convert.ToDateTime(value);
                        else if (field.ToLower() == "score")
                            Score = Convert.ToDouble(value);
                    }
                }
            }
        }
        var bfilter = builder.Empty;
        if (CustomerID != null)
        {
            var CustomerIDFilter = builder.Eq(x => x.CustomerID, CustomerID);
            bfilter &= CustomerIDFilter;
        }
        if (!string.IsNullOrWhiteSpace(Email))
        {
            var EmailFilter = builder.Eq(x => x.Email, Email);
            bfilter &= EmailFilter;
        }
        if (!string.IsNullOrWhiteSpace(Name))
        {
            var NameFilter = builder.Eq(x => x.Name, Name);
            bfilter &= NameFilter;
        }
        if (!string.IsNullOrWhiteSpace(Photo))
        {
            var PhotoFilter = builder.Eq(x => x.Photo, Photo);
            bfilter &= PhotoFilter;
        }
        if (BirthDate != null)
        {
            var BirthDateFilter = builder.Eq(x => x.BirthDate, BirthDate);
            bfilter &= BirthDateFilter;
        }
        if (Score != null)
        {
            var ScoreFilter = builder.Eq(x => x.Score, Score);
            bfilter &= ScoreFilter;
        }
        exp = bfilter;
        return exp;
    }
    public bool Exists(Guid customerProfileID)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var customerProfile = state.CustomerProfile.Find(x => x.ID == customerProfileID).Project<Model.CustomerProfile>("{ ID: 1 }").FirstOrDefault();
            return (customerProfileID == customerProfile?.ID);
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
            var total = state.CustomerProfile.Find(GetFilter(filter)).CountDocuments();
            return total;
        });
    }

#endregion Read

#region mappers
    public static DevPrime.State.Repositories.CustomerProfile.Model.CustomerProfile ToState(Domain.Aggregates.CustomerProfile.CustomerProfile customerProfile)
    {
        if (customerProfile is null)
            return new DevPrime.State.Repositories.CustomerProfile.Model.CustomerProfile();
        DevPrime.State.Repositories.CustomerProfile.Model.CustomerProfile _customerProfile = new DevPrime.State.Repositories.CustomerProfile.Model.CustomerProfile();
        _customerProfile.ID = customerProfile.ID;
        _customerProfile.CustomerID = customerProfile.CustomerID;
        _customerProfile.Email = customerProfile.Email;
        _customerProfile.Name = customerProfile.Name;
        _customerProfile.Photo = customerProfile.Photo;
        _customerProfile.BirthDate = customerProfile.BirthDate;
        _customerProfile.Score = customerProfile.Score;
        return _customerProfile;
    }
    public static Domain.Aggregates.CustomerProfile.CustomerProfile ToDomain(DevPrime.State.Repositories.CustomerProfile.Model.CustomerProfile customerProfile)
    {
        if (customerProfile is null)
            return new Domain.Aggregates.CustomerProfile.CustomerProfile()
            {IsNew = true};
        Domain.Aggregates.CustomerProfile.CustomerProfile _customerProfile = new Domain.Aggregates.CustomerProfile.CustomerProfile(customerProfile.ID, customerProfile.CustomerID, customerProfile.Email, customerProfile.Name, customerProfile.Photo, customerProfile.BirthDate, customerProfile.Score);
        return _customerProfile;
    }
    public static List<Domain.Aggregates.CustomerProfile.CustomerProfile> ToDomain(IList<DevPrime.State.Repositories.CustomerProfile.Model.CustomerProfile> customerProfileList)
    {
        List<Domain.Aggregates.CustomerProfile.CustomerProfile> _customerProfileList = new List<Domain.Aggregates.CustomerProfile.CustomerProfile>();
        if (customerProfileList != null)
        {
            foreach (var customerProfile in customerProfileList)
            {
                Domain.Aggregates.CustomerProfile.CustomerProfile _customerProfile = new Domain.Aggregates.CustomerProfile.CustomerProfile(customerProfile.ID, customerProfile.CustomerID, customerProfile.Email, customerProfile.Name, customerProfile.Photo, customerProfile.BirthDate, customerProfile.Score);
                _customerProfileList.Add(_customerProfile);
            }
        }
        return _customerProfileList;
    }

#endregion mappers
}