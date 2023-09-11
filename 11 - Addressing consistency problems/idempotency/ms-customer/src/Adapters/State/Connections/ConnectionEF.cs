namespace DevPrime.State.Connections;
public class ConnectionEF : EFBaseState
{
    public DbSet<DevPrime.State.Repositories.Customer.Model.Customer> Customer { get; set; }
    public ConnectionEF(DbContextOptions<ConnectionEF> options) : base(options)
    {
    }
    public ConnectionEF()
    {
    }
}