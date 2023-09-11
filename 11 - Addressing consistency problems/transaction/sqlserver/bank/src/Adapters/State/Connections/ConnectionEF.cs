namespace DevPrime.State.Connections;
public class ConnectionEF : EFBaseState
{
    public DbSet<DevPrime.State.Repositories.Account.Model.Account> Account { get; set; }
    public ConnectionEF(DbContextOptions<ConnectionEF> options) : base(options)
    {
    }

    public ConnectionEF()
    {
    }
}