namespace Application.Interfaces.Adapters.State;
public interface ICustomerProfileRepository
{
    bool Add(Domain.Aggregates.CustomerProfile.CustomerProfile source);
    bool Delete(Guid Id);
    bool Update(Domain.Aggregates.CustomerProfile.CustomerProfile source);
    Domain.Aggregates.CustomerProfile.CustomerProfile Get(Guid Id);
    List<Domain.Aggregates.CustomerProfile.CustomerProfile> GetAll(int? limit, int? offset, string ordering, string sort, string filter);
    bool Exists(Guid id);
    long Total(string filter);
}