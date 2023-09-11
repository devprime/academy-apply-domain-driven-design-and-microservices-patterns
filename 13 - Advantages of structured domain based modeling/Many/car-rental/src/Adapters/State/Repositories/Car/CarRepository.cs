namespace DevPrime.State.Repositories.Car;
public class CarRepository : RepositoryBase, ICarRepository
{
    public CarRepository(IDpState dp) : base(dp)
    {
        ConnectionAlias = "State1";
    }

#region Write
    public bool Add(Domain.Aggregates.Car.Car car)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var _car = ToState(car);
            state.Car.InsertOne(_car);
            return true;
        });
        if (result is null)
            return false;
        return result;
    }
    public bool Delete(Guid carID)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            state.Car.DeleteOne(p => p.ID == carID);
            return true;
        });
        if (result is null)
            return false;
        return result;
    }
    public bool Update(Domain.Aggregates.Car.Car car)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var _car = ToState(car);
            _car._Id = state.Car.Find(p => p.ID == car.ID).FirstOrDefault()._Id;
            state.Car.ReplaceOne(p => p.ID == car.ID, _car);
            return true;
        });
        if (result is null)
            return false;
        return result;
    }

#endregion Write

#region Read
    public Domain.Aggregates.Car.Car Get(Guid carID)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var car = state.Car.Find(p => p.ID == carID).FirstOrDefault();
            var _car = ToDomain(car);
            return _car;
        });
    }
    public List<Domain.Aggregates.Car.Car> GetAll(int? limit, int? offset, string ordering, string sort, string filter)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            List<Model.Car> car = null;
            if (sort?.ToLower() == "desc")
            {
                var result = state.Car.Find(GetFilter(filter)).SortByDescending(GetOrdering(ordering));
                if (limit != null && offset != null)
                    car = result.Skip((offset - 1) * limit).Limit(limit).ToList();
                else
                    car = result.ToList();
            }
            else if (sort?.ToLower() == "asc")
            {
                var result = state.Car.Find(GetFilter(filter)).SortBy(GetOrdering(ordering));
                if (limit != null && offset != null)
                    car = result.Skip((offset - 1) * limit).Limit(limit).ToList();
                else
                    car = result.ToList();
            }
            else
            {
                var result = state.Car.Find(GetFilter(filter));
                if (limit != null && offset != null)
                    car = result.Skip((offset - 1) * limit).Limit(limit).ToList();
                else
                    car = result.ToList();
            }
            var _car = ToDomain(car);
            return _car;
        });
    }
    private Expression<Func<Model.Car, object>> GetOrdering(string field)
    {
        Expression<Func<Model.Car, object>> exp = p => p.ID;
        if (!string.IsNullOrWhiteSpace(field))
        {
            if (field.ToLower() == "model")
                exp = p => p.Model;
            else if (field.ToLower() == "licenseplate")
                exp = p => p.LicensePlate;
            else
                exp = p => p.ID;
        }
        return exp;
    }
    private FilterDefinition<Model.Car> GetFilter(string filter)
    {
        var builder = Builders<Model.Car>.Filter;
        FilterDefinition<Model.Car> exp;
        string Model = string.Empty;
        string LicensePlate = string.Empty;
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
                        if (field.ToLower() == "model")
                            Model = value;
                        else if (field.ToLower() == "licenseplate")
                            LicensePlate = value;
                    }
                }
            }
        }
        var bfilter = builder.Empty;
        if (!string.IsNullOrWhiteSpace(Model))
        {
            var ModelFilter = builder.Eq(x => x.Model, Model);
            bfilter &= ModelFilter;
        }
        if (!string.IsNullOrWhiteSpace(LicensePlate))
        {
            var LicensePlateFilter = builder.Eq(x => x.LicensePlate, LicensePlate);
            bfilter &= LicensePlateFilter;
        }
        exp = bfilter;
        return exp;
    }
    public bool Exists(Guid carID)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var car = state.Car.Find(x => x.ID == carID).Project<Model.Car>("{ ID: 1 }").FirstOrDefault();
            return (carID == car?.ID);
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
            var total = state.Car.Find(GetFilter(filter)).CountDocuments();
            return total;
        });
    }

#endregion Read

#region mappers
    public static DevPrime.State.Repositories.Car.Model.Car ToState(Domain.Aggregates.Car.Car car)
    {
        if (car is null)
            return new DevPrime.State.Repositories.Car.Model.Car();
        DevPrime.State.Repositories.Car.Model.Car _car = new DevPrime.State.Repositories.Car.Model.Car();
        _car.ID = car.ID;
        _car.Model = car.Model;
        _car.LicensePlate = car.LicensePlate;
        return _car;
    }
    public static Domain.Aggregates.Car.Car ToDomain(DevPrime.State.Repositories.Car.Model.Car car)
    {
        if (car is null)
            return new Domain.Aggregates.Car.Car()
            {IsNew = true};
        Domain.Aggregates.Car.Car _car = new Domain.Aggregates.Car.Car(car.ID, car.Model, car.LicensePlate);
        return _car;
    }
    public static List<Domain.Aggregates.Car.Car> ToDomain(IList<DevPrime.State.Repositories.Car.Model.Car> carList)
    {
        List<Domain.Aggregates.Car.Car> _carList = new List<Domain.Aggregates.Car.Car>();
        if (carList != null)
        {
            foreach (var car in carList)
            {
                Domain.Aggregates.Car.Car _car = new Domain.Aggregates.Car.Car(car.ID, car.Model, car.LicensePlate);
                _carList.Add(_car);
            }
        }
        return _carList;
    }

#endregion mappers
}