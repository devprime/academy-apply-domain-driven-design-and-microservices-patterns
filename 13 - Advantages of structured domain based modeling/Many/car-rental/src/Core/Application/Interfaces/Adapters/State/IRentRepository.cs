namespace Application.Interfaces.Adapters.State;
public interface IRentRepository
{
    bool Add(Domain.Aggregates.Rent.Rent source);
    bool Delete(Guid Id);
    bool Update(Domain.Aggregates.Rent.Rent source);
    Domain.Aggregates.Rent.Rent Get(Guid Id);
    List<Domain.Aggregates.Rent.Rent> GetAll(int? limit, int? offset, string ordering, string sort, string filter);
    bool Exists(Guid id);
    long Total(string filter);
}