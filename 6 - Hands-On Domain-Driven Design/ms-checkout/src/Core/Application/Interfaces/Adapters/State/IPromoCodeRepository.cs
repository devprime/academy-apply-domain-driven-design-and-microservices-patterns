namespace Application.Interfaces.Adapters.State;
public interface IPromoCodeRepository
{
    bool Add(Domain.Aggregates.PromoCode.PromoCode source);
    bool Delete(Guid Id);
    bool Update(Domain.Aggregates.PromoCode.PromoCode source);
    Domain.Aggregates.PromoCode.PromoCode Get(Guid Id);
    List<Domain.Aggregates.PromoCode.PromoCode> GetAll(int? limit, int? offset, string ordering, string sort, string filter);
    bool Exists(Guid id);
    long Total(string filter);
    Domain.Aggregates.PromoCode.PromoCode GetByCode(string code);
}