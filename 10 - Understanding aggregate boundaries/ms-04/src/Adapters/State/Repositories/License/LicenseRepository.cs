namespace DevPrime.State.Repositories.License;
public class LicenseRepository : RepositoryBase, ILicenseRepository
{
    public LicenseRepository(IDpState dp) : base(dp)
    {
        ConnectionAlias = "State1";
    }

#region Write
    public bool Add(Domain.Aggregates.License.License license)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var _license = ToState(license);
            state.License.InsertOne(_license);
            return true;
        });
        if (result is null)
            return false;
        return result;
    }
    public bool Delete(Guid licenseID)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            state.License.DeleteOne(p => p.ID == licenseID);
            return true;
        });
        if (result is null)
            return false;
        return result;
    }
    public bool Update(Domain.Aggregates.License.License license)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var _license = ToState(license);
            _license._Id = state.License.Find(p => p.ID == license.ID).FirstOrDefault()._Id;
            state.License.ReplaceOne(p => p.ID == license.ID, _license);
            return true;
        });
        if (result is null)
            return false;
        return result;
    }

#endregion Write

#region Read
    public Domain.Aggregates.License.License Get(Guid licenseID)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var license = state.License.Find(p => p.ID == licenseID).FirstOrDefault();
            var _license = ToDomain(license);
            return _license;
        });
    }
    public List<Domain.Aggregates.License.License> GetAll(int? limit, int? offset, string ordering, string sort, string filter)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            List<Model.License> license = null;
            if (sort?.ToLower() == "desc")
            {
                var result = state.License.Find(GetFilter(filter)).SortByDescending(GetOrdering(ordering));
                if (limit != null && offset != null)
                    license = result.Skip((offset - 1) * limit).Limit(limit).ToList();
                else
                    license = result.ToList();
            }
            else if (sort?.ToLower() == "asc")
            {
                var result = state.License.Find(GetFilter(filter)).SortBy(GetOrdering(ordering));
                if (limit != null && offset != null)
                    license = result.Skip((offset - 1) * limit).Limit(limit).ToList();
                else
                    license = result.ToList();
            }
            else
            {
                var result = state.License.Find(GetFilter(filter));
                if (limit != null && offset != null)
                    license = result.Skip((offset - 1) * limit).Limit(limit).ToList();
                else
                    license = result.ToList();
            }
            var _license = ToDomain(license);
            return _license;
        });
    }
    private Expression<Func<Model.License, object>> GetOrdering(string field)
    {
        Expression<Func<Model.License, object>> exp = p => p.ID;
        if (!string.IsNullOrWhiteSpace(field))
        {
            if (field.ToLower() == "description")
                exp = p => p.Description;
            else
                exp = p => p.ID;
        }
        return exp;
    }
    private FilterDefinition<Model.License> GetFilter(string filter)
    {
        var builder = Builders<Model.License>.Filter;
        FilterDefinition<Model.License> exp;
        string Description = string.Empty;
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
                        if (field.ToLower() == "description")
                            Description = value;
                    }
                }
            }
        }
        var bfilter = builder.Empty;
        if (!string.IsNullOrWhiteSpace(Description))
        {
            var DescriptionFilter = builder.Eq(x => x.Description, Description);
            bfilter &= DescriptionFilter;
        }
        exp = bfilter;
        return exp;
    }
    public bool Exists(Guid licenseID)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var license = state.License.Find(x => x.ID == licenseID).Project<Model.License>("{ ID: 1 }").FirstOrDefault();
            return (licenseID == license?.ID);
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
            var total = state.License.Find(GetFilter(filter)).CountDocuments();
            return total;
        });
    }

#endregion Read

#region mappers
    public static DevPrime.State.Repositories.License.Model.License ToState(Domain.Aggregates.License.License license)
    {
        if (license is null)
            return new DevPrime.State.Repositories.License.Model.License();
        DevPrime.State.Repositories.License.Model.License _license = new DevPrime.State.Repositories.License.Model.License();
        _license.ID = license.ID;
        _license.Description = license.Description;
        _license.Type = license.Type.ToString();
        return _license;
    }
    public static Domain.Aggregates.License.License ToDomain(DevPrime.State.Repositories.License.Model.License license)
    {
        if (license is null)
            return new Domain.Aggregates.License.License()
            {IsNew = true};
        Domain.Aggregates.License.License _license = new Domain.Aggregates.License.License(license.ID, license.Description, license.Type);
        return _license;
    }
    public static List<Domain.Aggregates.License.License> ToDomain(IList<DevPrime.State.Repositories.License.Model.License> licenseList)
    {
        List<Domain.Aggregates.License.License> _licenseList = new List<Domain.Aggregates.License.License>();
        if (licenseList != null)
        {
            foreach (var license in licenseList)
            {
                Domain.Aggregates.License.License _license = new Domain.Aggregates.License.License(license.ID, license.Description, license.Type);
                _licenseList.Add(_license);
            }
        }
        return _licenseList;
    }

#endregion mappers
}