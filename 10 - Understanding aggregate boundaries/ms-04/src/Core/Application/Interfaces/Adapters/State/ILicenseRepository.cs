namespace Application.Interfaces.Adapters.State;
public interface ILicenseRepository
{
    bool Add(Domain.Aggregates.License.License source);
    bool Delete(Guid Id);
    bool Update(Domain.Aggregates.License.License source);
    Domain.Aggregates.License.License Get(Guid Id);
    List<Domain.Aggregates.License.License> GetAll(int? limit, int? offset, string ordering, string sort, string filter);
    bool Exists(Guid id);
    long Total(string filter);
}