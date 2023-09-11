namespace Application.Interfaces.Adapters.State;
public interface IAccountRepository
{
    Domain.Aggregates.Account.Account GetByNumber(string number);
    void Add(Domain.Aggregates.Account.Account source);
    void Delete(Guid Id);
    void Update(Domain.Aggregates.Account.Account source);
    Domain.Aggregates.Account.Account Get(Guid Id);
    List<Domain.Aggregates.Account.Account> GetAll(int? limit, int? offset, string ordering, string sort, string filter);
    bool Exists(Guid id);
    long Total(string filter);
}