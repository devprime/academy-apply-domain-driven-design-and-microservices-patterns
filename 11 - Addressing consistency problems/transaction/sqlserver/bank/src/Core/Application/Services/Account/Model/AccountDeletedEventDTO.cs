namespace Application.Services.Account.Model;
public class AccountDeletedEventDTO
{
    public Guid ID { get; set; }
    public string Number { get; set; }
    public double Balance { get; set; }
}