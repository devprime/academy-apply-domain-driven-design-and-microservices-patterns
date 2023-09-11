namespace Application.Services.Rent;
public class RentService : ApplicationService<IRentState>, IRentService
{
    public RentService(IRentState state, IDp dp) : base(state, dp)
    {
    }
    public void Add(Model.Rent command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var rent = command.ToDomain();
            Dp.Attach(rent);
            rent.Add();
        });
    }
    public void Update(Model.Rent command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var rent = command.ToDomain();
            Dp.Attach(rent);
            rent.Update();
        });
    }
    public void Delete(Model.Rent command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var rent = command.ToDomain();
            Dp.Attach(rent);
            rent.Delete();
        });
    }
    public PagingResult<IList<Model.Rent>> GetAll(Model.Rent query)
    {
        return Dp.Pipeline(ExecuteResult: () =>
        {
            var rent = query.ToDomain();
            Dp.Attach(rent);
            var rentList = rent.Get(query.Limit, query.Offset, query.Ordering, query.Sort, query.Filter);
            var result = query.ToRentList(rentList.Result, rentList.Total, query.Offset, query.Limit);
            return result;
        });
    }
    public Model.Rent Get(Model.Rent query)
    {
        return Dp.Pipeline(ExecuteResult: () =>
        {
            var rent = query.ToDomain();
            Dp.Attach(rent);
            rent = rent.GetByID();
            var result = query.ToRent(rent);
            return result;
        });
    }
}