namespace DevPrime.State.Connections;
public class ConnectionMongo : MongoBaseState
{
    public ConnectionMongo(MongoBaseState stateContext, IDpState dp) : base(stateContext, dp)
    {
    }
    public IMongoCollection<DevPrime.State.Repositories.License.Model.License> License
    {
        get
        {
            return Db.GetCollection<DevPrime.State.Repositories.License.Model.License>("License");
        }
    }
    public IMongoCollection<DevPrime.State.Repositories.User.Model.User> User
    {
        get
        {
            return Db.GetCollection<DevPrime.State.Repositories.User.Model.User>("User");
        }
    }
    public IMongoCollection<DevPrime.State.Repositories.UserLicense.Model.UserLicense> UserLicense
    {
        get
        {
            return Db.GetCollection<DevPrime.State.Repositories.UserLicense.Model.UserLicense>("UserLicense");
        }
    }
}