namespace Application.Services.Order;
public class OrderService : ApplicationService<IOrderState>, IOrderService
{
    private ICheckout Checkout {get; set;}
    public OrderService(ICheckout checkout, IOrderState state, IDp dp) : base(state, dp)
    {
        Checkout = checkout;
    }
    public void Add(Model.Order command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var order = command.ToDomain();
            Dp.Attach(order);

            Checkout.Buy(order);
        });
    }
    
    public void Update(Model.Order command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var order = command.ToDomain();
            Dp.Attach(order);
            order.Update();
        });
    }
    public void Delete(Model.Order command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var order = command.ToDomain();
            Dp.Attach(order);
            order.Delete();
        });
    }
    public PagingResult<IList<Model.Order>> GetAll(Model.Order query)
    {
        return Dp.Pipeline(ExecuteResult: () =>
        {
            var order = query.ToDomain();
            Dp.Attach(order);
            var orderList = order.Get(query.Limit, query.Offset, query.Ordering, query.Sort, query.Filter);
            var result = query.ToOrderList(orderList.Result, orderList.Total, query.Offset, query.Limit);
            return result;
        });
    }
    public Model.Order Get(Model.Order query)
    {
        return Dp.Pipeline(ExecuteResult: () =>
        {
            var order = query.ToDomain();
            Dp.Attach(order);
            order = order.GetByID();
            var result = query.ToOrder(order);
            return result;
        });
    }
}