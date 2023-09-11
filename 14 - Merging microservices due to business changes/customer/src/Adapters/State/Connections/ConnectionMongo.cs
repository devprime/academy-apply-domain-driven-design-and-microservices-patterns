namespace DevPrime.State.Connections;
public class ConnectionMongo : MongoBaseState
{
    public ConnectionMongo(MongoBaseState stateContext, IDpState dp) : base(stateContext, dp)
    {
    }
    public IMongoCollection<DevPrime.State.Repositories.CustomerProfile.Model.CustomerProfile> CustomerProfile
    {
        get
        {
            return Db.GetCollection<DevPrime.State.Repositories.CustomerProfile.Model.CustomerProfile>("CustomerProfile");
        }
    }
    public IMongoCollection<DevPrime.State.Repositories.Customer.Model.Customer> Customer
    {
        get
        {
            return Db.GetCollection<DevPrime.State.Repositories.Customer.Model.Customer>("Customer");
        }
    }
}