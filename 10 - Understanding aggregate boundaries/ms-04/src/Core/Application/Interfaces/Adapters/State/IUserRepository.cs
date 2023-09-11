namespace Application.Interfaces.Adapters.State;
public interface IUserRepository
{
    bool Add(Domain.Aggregates.User.User source);
    bool Delete(Guid Id);
    bool Update(Domain.Aggregates.User.User source);
    Domain.Aggregates.User.User Get(Guid Id);
    List<Domain.Aggregates.User.User> GetAll(int? limit, int? offset, string ordering, string sort, string filter);
    bool Exists(Guid id);
    long Total(string filter);
}