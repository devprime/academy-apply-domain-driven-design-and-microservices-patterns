namespace Domain.Aggregates.CustomerProfile.Events;
public class GetCustomerInformation : DomainEvent
{
    public Guid CustomerID { get; set;}
    public GetCustomerInformation(Guid customerid) : base()
    {
        CustomerID = customerid;
    }
}
