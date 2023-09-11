namespace Application.Interfaces.Adapters.State;
public interface IUserLicenseRepository
{
    bool Add(Domain.Aggregates.UserLicense.UserLicense source);
    bool Delete(Guid Id);
    bool Update(Domain.Aggregates.UserLicense.UserLicense source);
    Domain.Aggregates.UserLicense.UserLicense Get(Guid Id);
    List<Domain.Aggregates.UserLicense.UserLicense> GetAll(int? limit, int? offset, string ordering, string sort, string filter);
    bool Exists(Guid id);
    long Total(string filter);
}