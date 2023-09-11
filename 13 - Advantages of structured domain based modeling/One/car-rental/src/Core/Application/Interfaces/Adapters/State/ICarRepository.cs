namespace Application.Interfaces.Adapters.State;
public interface ICarRepository
{
    bool Add(Domain.Aggregates.Car.Car source);
    bool Delete(Guid Id);
    bool Update(Domain.Aggregates.Car.Car source);
    Domain.Aggregates.Car.Car Get(Guid Id);
    List<Domain.Aggregates.Car.Car> GetAll(int? limit, int? offset, string ordering, string sort, string filter);
    bool Exists(Guid id);
    long Total(string filter);
}