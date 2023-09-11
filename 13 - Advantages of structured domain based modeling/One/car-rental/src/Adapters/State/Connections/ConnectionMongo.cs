namespace DevPrime.State.Connections;
public class ConnectionMongo : MongoBaseState
{
    public ConnectionMongo(MongoBaseState stateContext, IDpState dp) : base(stateContext, dp)
    {
    }
    public IMongoCollection<DevPrime.State.Repositories.Car.Model.Car> Car
    {
        get
        {
            return Db.GetCollection<DevPrime.State.Repositories.Car.Model.Car>("Car");
        }
    }
    public IMongoCollection<DevPrime.State.Repositories.Customer.Model.Customer> Customer
    {
        get
        {
            return Db.GetCollection<DevPrime.State.Repositories.Customer.Model.Customer>("Customer");
        }
    }
    public IMongoCollection<DevPrime.State.Repositories.Rent.Model.Rent> Rent
    {
        get
        {
            return Db.GetCollection<DevPrime.State.Repositories.Rent.Model.Rent>("Rent");
        }
    }
}