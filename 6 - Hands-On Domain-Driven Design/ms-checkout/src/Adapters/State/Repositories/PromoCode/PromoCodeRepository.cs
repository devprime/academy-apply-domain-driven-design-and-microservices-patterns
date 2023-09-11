namespace DevPrime.State.Repositories.PromoCode;
public class PromoCodeRepository : RepositoryBase, IPromoCodeRepository
{
    public PromoCodeRepository(IDpState dp) : base(dp)
    {
        ConnectionAlias = "State1";
        //I don t want to lost my changes in previous code
    }

#region Write
    public bool Add(Domain.Aggregates.PromoCode.PromoCode promoCode)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var _promoCode = ToState(promoCode);
            state.PromoCode.InsertOne(_promoCode);
            return true;
        });
        if (result is null)
            return false;
        return result;
    }
    public bool Delete(Guid promoCodeID)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            state.PromoCode.DeleteOne(p => p.ID == promoCodeID);
            return true;
        });
        if (result is null)
            return false;
        return result;
    }
    public bool Update(Domain.Aggregates.PromoCode.PromoCode promoCode)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var _promoCode = ToState(promoCode);
            _promoCode._Id = state.PromoCode.Find(p => p.ID == promoCode.ID).FirstOrDefault()._Id;
            state.PromoCode.ReplaceOne(p => p.ID == promoCode.ID, _promoCode);
            return true;
        });
        if (result is null)
            return false;
        return result;
    }

#endregion Write

#region Read

    public Domain.Aggregates.PromoCode.PromoCode GetByCode(string code)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var promoCode = state.PromoCode.Find(p => p.Code == code).FirstOrDefault();
            var _promoCode = ToDomain(promoCode);
            return _promoCode;
        });
    }
    public Domain.Aggregates.PromoCode.PromoCode Get(Guid promoCodeID)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var promoCode = state.PromoCode.Find(p => p.ID == promoCodeID).FirstOrDefault();
            var _promoCode = ToDomain(promoCode);
            return _promoCode;
        });
    }
    public List<Domain.Aggregates.PromoCode.PromoCode> GetAll(int? limit, int? offset, string ordering, string sort, string filter)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            List<Model.PromoCode> promoCode = null;
            if (sort?.ToLower() == "desc")
            {
                var result = state.PromoCode.Find(GetFilter(filter)).SortByDescending(GetOrdering(ordering));
                if (limit != null && offset != null)
                    promoCode = result.Skip((offset - 1) * limit).Limit(limit).ToList();
                else
                    promoCode = result.ToList();
            }
            else if (sort?.ToLower() == "asc")
            {
                var result = state.PromoCode.Find(GetFilter(filter)).SortBy(GetOrdering(ordering));
                if (limit != null && offset != null)
                    promoCode = result.Skip((offset - 1) * limit).Limit(limit).ToList();
                else
                    promoCode = result.ToList();
            }
            else
            {
                var result = state.PromoCode.Find(GetFilter(filter));
                if (limit != null && offset != null)
                    promoCode = result.Skip((offset - 1) * limit).Limit(limit).ToList();
                else
                    promoCode = result.ToList();
            }
            var _promoCode = ToDomain(promoCode);
            return _promoCode;
        });
    }
    private Expression<Func<Model.PromoCode, object>> GetOrdering(string field)
    {
        Expression<Func<Model.PromoCode, object>> exp = p => p.ID;
        if (!string.IsNullOrWhiteSpace(field))
        {
            if (field.ToLower() == "code")
                exp = p => p.Code;
            else if (field.ToLower() == "percentagediscount")
                exp = p => p.PercentageDiscount;
            else if (field.ToLower() == "active")
                exp = p => p.Active;
            else if (field.ToLower() == "validuntil")
                exp = p => p.ValidUntil;
            else
                exp = p => p.ID;
        }
        return exp;
    }
    private FilterDefinition<Model.PromoCode> GetFilter(string filter)
    {
        var builder = Builders<Model.PromoCode>.Filter;
        FilterDefinition<Model.PromoCode> exp;
        string Code = string.Empty;
        Double? PercentageDiscount = null;
        Boolean? Active = null;
        DateTime? ValidUntil = null;
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
                        if (field.ToLower() == "code")
                            Code = value;
                        else if (field.ToLower() == "percentagediscount")
                            PercentageDiscount = Convert.ToDouble(value);
                        else if (field.ToLower() == "active")
                            Active = Convert.ToBoolean(value);
                        else if (field.ToLower() == "validuntil")
                            ValidUntil = Convert.ToDateTime(value);
                    }
                }
            }
        }
        var bfilter = builder.Empty;
        if (!string.IsNullOrWhiteSpace(Code))
        {
            var CodeFilter = builder.Eq(x => x.Code, Code);
            bfilter &= CodeFilter;
        }
        if (PercentageDiscount != null)
        {
            var PercentageDiscountFilter = builder.Eq(x => x.PercentageDiscount, PercentageDiscount);
            bfilter &= PercentageDiscountFilter;
        }
        if (Active != null)
        {
            var ActiveFilter = builder.Eq(x => x.Active, Active);
            bfilter &= ActiveFilter;
        }
        if (ValidUntil != null)
        {
            var ValidUntilFilter = builder.Eq(x => x.ValidUntil, ValidUntil);
            bfilter &= ValidUntilFilter;
        }
        exp = bfilter;
        return exp;
    }
    public bool Exists(Guid promoCodeID)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var promoCode = state.PromoCode.Find(x => x.ID == promoCodeID).Project<Model.PromoCode>("{ ID: 1 }").FirstOrDefault();
            return (promoCodeID == promoCode?.ID);
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
            var total = state.PromoCode.Find(GetFilter(filter)).CountDocuments();
            return total;
        });
    }

#endregion Read

#region mappers
    public static DevPrime.State.Repositories.PromoCode.Model.PromoCode ToState(Domain.Aggregates.PromoCode.PromoCode promoCode)
    {
        if (promoCode is null)
            return new DevPrime.State.Repositories.PromoCode.Model.PromoCode();
        DevPrime.State.Repositories.PromoCode.Model.PromoCode _promoCode = new DevPrime.State.Repositories.PromoCode.Model.PromoCode();
        _promoCode.ID = promoCode.ID;
        _promoCode.Code = promoCode.Code;
        _promoCode.PercentageDiscount = promoCode.PercentageDiscount;
        _promoCode.Active = promoCode.Active;
        _promoCode.ValidUntil = promoCode.ValidUntil;
        return _promoCode;
    }
    public static Domain.Aggregates.PromoCode.PromoCode ToDomain(DevPrime.State.Repositories.PromoCode.Model.PromoCode promoCode)
    {
        if (promoCode is null)
            return new Domain.Aggregates.PromoCode.PromoCode()
            {IsNew = true};
        Domain.Aggregates.PromoCode.PromoCode _promoCode = new Domain.Aggregates.PromoCode.PromoCode(promoCode.ID, promoCode.Code, promoCode.PercentageDiscount, promoCode.Active, promoCode.ValidUntil);
        return _promoCode;
    }
    public static List<Domain.Aggregates.PromoCode.PromoCode> ToDomain(IList<DevPrime.State.Repositories.PromoCode.Model.PromoCode> promoCodeList)
    {
        List<Domain.Aggregates.PromoCode.PromoCode> _promoCodeList = new List<Domain.Aggregates.PromoCode.PromoCode>();
        if (promoCodeList != null)
        {
            foreach (var promoCode in promoCodeList)
            {
                Domain.Aggregates.PromoCode.PromoCode _promoCode = new Domain.Aggregates.PromoCode.PromoCode(promoCode.ID, promoCode.Code, promoCode.PercentageDiscount, promoCode.Active, promoCode.ValidUntil);
                _promoCodeList.Add(_promoCode);
            }
        }
        return _promoCodeList;
    }

#endregion mappers
}