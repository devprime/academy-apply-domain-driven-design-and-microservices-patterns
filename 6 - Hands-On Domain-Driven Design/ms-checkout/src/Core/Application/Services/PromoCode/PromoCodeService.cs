namespace Application.Services.PromoCode;
public class PromoCodeService : ApplicationService<IPromoCodeState>, IPromoCodeService
{
    public PromoCodeService(IPromoCodeState state, IDp dp) : base(state, dp)
    {
    }
    public void Add(Model.PromoCode command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var promoCode = command.ToDomain();
            Dp.Attach(promoCode);
            promoCode.Add();
        });
    }
    public void Update(Model.PromoCode command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var promoCode = command.ToDomain();
            Dp.Attach(promoCode);
            promoCode.Update();
        });
    }
    public void Delete(Model.PromoCode command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var promoCode = command.ToDomain();
            Dp.Attach(promoCode);
            promoCode.Delete();
        });
    }
    public PagingResult<IList<Model.PromoCode>> GetAll(Model.PromoCode query)
    {
        return Dp.Pipeline(ExecuteResult: () =>
        {
            var promoCode = query.ToDomain();
            Dp.Attach(promoCode);
            var promoCodeList = promoCode.Get(query.Limit, query.Offset, query.Ordering, query.Sort, query.Filter);
            var result = query.ToPromoCodeList(promoCodeList.Result, promoCodeList.Total, query.Offset, query.Limit);
            return result;
        });
    }
    public Model.PromoCode Get(Model.PromoCode query)
    {
        return Dp.Pipeline(ExecuteResult: () =>
        {
            var promoCode = query.ToDomain();
            Dp.Attach(promoCode);
            promoCode = promoCode.GetByID();
            var result = query.ToPromoCode(promoCode);
            return result;
        });
    }
}