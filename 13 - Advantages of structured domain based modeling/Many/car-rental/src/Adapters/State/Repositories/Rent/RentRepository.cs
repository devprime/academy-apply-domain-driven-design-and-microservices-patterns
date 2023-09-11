namespace DevPrime.State.Repositories.Rent;
public class RentRepository : RepositoryBase, IRentRepository
{
    public RentRepository(IDpState dp) : base(dp)
    {
        ConnectionAlias = "State1";
    }

#region Write
    public bool Add(Domain.Aggregates.Rent.Rent rent)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var _rent = ToState(rent);
            state.Rent.InsertOne(_rent);
            return true;
        });
        if (result is null)
            return false;
        return result;
    }
    public bool Delete(Guid rentID)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            state.Rent.DeleteOne(p => p.ID == rentID);
            return true;
        });
        if (result is null)
            return false;
        return result;
    }
    public bool Update(Domain.Aggregates.Rent.Rent rent)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var _rent = ToState(rent);
            _rent._Id = state.Rent.Find(p => p.ID == rent.ID).FirstOrDefault()._Id;
            state.Rent.ReplaceOne(p => p.ID == rent.ID, _rent);
            return true;
        });
        if (result is null)
            return false;
        return result;
    }

#endregion Write

#region Read
    public Domain.Aggregates.Rent.Rent Get(Guid rentID)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var rent = state.Rent.Find(p => p.ID == rentID).FirstOrDefault();
            var _rent = ToDomain(rent);
            return _rent;
        });
    }
    public List<Domain.Aggregates.Rent.Rent> GetAll(int? limit, int? offset, string ordering, string sort, string filter)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            List<Model.Rent> rent = null;
            if (sort?.ToLower() == "desc")
            {
                var result = state.Rent.Find(GetFilter(filter)).SortByDescending(GetOrdering(ordering));
                if (limit != null && offset != null)
                    rent = result.Skip((offset - 1) * limit).Limit(limit).ToList();
                else
                    rent = result.ToList();
            }
            else if (sort?.ToLower() == "asc")
            {
                var result = state.Rent.Find(GetFilter(filter)).SortBy(GetOrdering(ordering));
                if (limit != null && offset != null)
                    rent = result.Skip((offset - 1) * limit).Limit(limit).ToList();
                else
                    rent = result.ToList();
            }
            else
            {
                var result = state.Rent.Find(GetFilter(filter));
                if (limit != null && offset != null)
                    rent = result.Skip((offset - 1) * limit).Limit(limit).ToList();
                else
                    rent = result.ToList();
            }
            var _rent = ToDomain(rent);
            return _rent;
        });
    }
    private Expression<Func<Model.Rent, object>> GetOrdering(string field)
    {
        Expression<Func<Model.Rent, object>> exp = p => p.ID;
        if (!string.IsNullOrWhiteSpace(field))
        {
            if (field.ToLower() == "licenseplate")
                exp = p => p.LicensePlate;
            else if (field.ToLower() == "taxid")
                exp = p => p.TaxID;
            else if (field.ToLower() == "start")
                exp = p => p.Start;
            else if (field.ToLower() == "end")
                exp = p => p.End;
            else
                exp = p => p.ID;
        }
        return exp;
    }
    private FilterDefinition<Model.Rent> GetFilter(string filter)
    {
        var builder = Builders<Model.Rent>.Filter;
        FilterDefinition<Model.Rent> exp;
        string LicensePlate = string.Empty;
        string TaxID = string.Empty;
        DateTime? Start = null;
        DateTime? End = null;
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
                        if (field.ToLower() == "licenseplate")
                            LicensePlate = value;
                        else if (field.ToLower() == "taxid")
                            TaxID = value;
                        else if (field.ToLower() == "start")
                            Start = Convert.ToDateTime(value);
                        else if (field.ToLower() == "end")
                            End = Convert.ToDateTime(value);
                    }
                }
            }
        }
        var bfilter = builder.Empty;
        if (!string.IsNullOrWhiteSpace(LicensePlate))
        {
            var LicensePlateFilter = builder.Eq(x => x.LicensePlate, LicensePlate);
            bfilter &= LicensePlateFilter;
        }
        if (!string.IsNullOrWhiteSpace(TaxID))
        {
            var TaxIDFilter = builder.Eq(x => x.TaxID, TaxID);
            bfilter &= TaxIDFilter;
        }
        if (Start != null)
        {
            var StartFilter = builder.Eq(x => x.Start, Start);
            bfilter &= StartFilter;
        }
        if (End != null)
        {
            var EndFilter = builder.Eq(x => x.End, End);
            bfilter &= EndFilter;
        }
        exp = bfilter;
        return exp;
    }
    public bool Exists(Guid rentID)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var rent = state.Rent.Find(x => x.ID == rentID).Project<Model.Rent>("{ ID: 1 }").FirstOrDefault();
            return (rentID == rent?.ID);
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
            var total = state.Rent.Find(GetFilter(filter)).CountDocuments();
            return total;
        });
    }

#endregion Read

#region mappers
    public static DevPrime.State.Repositories.Rent.Model.Rent ToState(Domain.Aggregates.Rent.Rent rent)
    {
        if (rent is null)
            return new DevPrime.State.Repositories.Rent.Model.Rent();
        DevPrime.State.Repositories.Rent.Model.Rent _rent = new DevPrime.State.Repositories.Rent.Model.Rent();
        _rent.ID = rent.ID;
        _rent.LicensePlate = rent.LicensePlate;
        _rent.TaxID = rent.TaxID;
        _rent.Start = rent.Start;
        _rent.End = rent.End;
        return _rent;
    }
    public static Domain.Aggregates.Rent.Rent ToDomain(DevPrime.State.Repositories.Rent.Model.Rent rent)
    {
        if (rent is null)
            return new Domain.Aggregates.Rent.Rent()
            {IsNew = true};
        Domain.Aggregates.Rent.Rent _rent = new Domain.Aggregates.Rent.Rent(rent.ID, rent.LicensePlate, rent.TaxID, rent.Start, rent.End);
        return _rent;
    }
    public static List<Domain.Aggregates.Rent.Rent> ToDomain(IList<DevPrime.State.Repositories.Rent.Model.Rent> rentList)
    {
        List<Domain.Aggregates.Rent.Rent> _rentList = new List<Domain.Aggregates.Rent.Rent>();
        if (rentList != null)
        {
            foreach (var rent in rentList)
            {
                Domain.Aggregates.Rent.Rent _rent = new Domain.Aggregates.Rent.Rent(rent.ID, rent.LicensePlate, rent.TaxID, rent.Start, rent.End);
                _rentList.Add(_rent);
            }
        }
        return _rentList;
    }

#endregion mappers
}