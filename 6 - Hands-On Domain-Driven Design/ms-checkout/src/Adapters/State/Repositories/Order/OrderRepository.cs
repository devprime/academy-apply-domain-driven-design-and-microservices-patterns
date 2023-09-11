namespace DevPrime.State.Repositories.Order;
public class OrderRepository : RepositoryBase, IOrderRepository
{
    public OrderRepository(IDpState dp) : base(dp)
    {
        ConnectionAlias = "State1";
    }

#region Write
    public bool Add(Domain.Aggregates.Order.Order order)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var _order = ToState(order);
            state.Order.InsertOne(_order);
            return true;
        });
        if (result is null)
            return false;
        return result;
    }
    public bool Delete(Guid orderID)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            state.Order.DeleteOne(p => p.ID == orderID);
            return true;
        });
        if (result is null)
            return false;
        return result;
    }
    public bool Update(Domain.Aggregates.Order.Order order)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var _order = ToState(order);
            _order._Id = state.Order.Find(p => p.ID == order.ID).FirstOrDefault()._Id;
            state.Order.ReplaceOne(p => p.ID == order.ID, _order);
            return true;
        });
        if (result is null)
            return false;
        return result;
    }

#endregion Write

#region Read
    public Domain.Aggregates.Order.Order Get(Guid orderID)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var order = state.Order.Find(p => p.ID == orderID).FirstOrDefault();
            var _order = ToDomain(order);
            return _order;
        });
    }
    public List<Domain.Aggregates.Order.Order> GetAll(int? limit, int? offset, string ordering, string sort, string filter)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            List<Model.Order> order = null;
            if (sort?.ToLower() == "desc")
            {
                var result = state.Order.Find(GetFilter(filter)).SortByDescending(GetOrdering(ordering));
                if (limit != null && offset != null)
                    order = result.Skip((offset - 1) * limit).Limit(limit).ToList();
                else
                    order = result.ToList();
            }
            else if (sort?.ToLower() == "asc")
            {
                var result = state.Order.Find(GetFilter(filter)).SortBy(GetOrdering(ordering));
                if (limit != null && offset != null)
                    order = result.Skip((offset - 1) * limit).Limit(limit).ToList();
                else
                    order = result.ToList();
            }
            else
            {
                var result = state.Order.Find(GetFilter(filter));
                if (limit != null && offset != null)
                    order = result.Skip((offset - 1) * limit).Limit(limit).ToList();
                else
                    order = result.ToList();
            }
            var _order = ToDomain(order);
            return _order;
        });
    }
    private Expression<Func<Model.Order, object>> GetOrdering(string field)
    {
        Expression<Func<Model.Order, object>> exp = p => p.ID;
        if (!string.IsNullOrWhiteSpace(field))
        {
            if (field.ToLower() == "customertaxid")
                exp = p => p.CustomerTaxID;
            else if (field.ToLower() == "customername")
                exp = p => p.CustomerName;
            else if (field.ToLower() == "promocode")
                exp = p => p.PromoCode;
            else if (field.ToLower() == "totalprice")
                exp = p => p.TotalPrice;
            else
                exp = p => p.ID;
        }
        return exp;
    }
    private FilterDefinition<Model.Order> GetFilter(string filter)
    {
        var builder = Builders<Model.Order>.Filter;
        FilterDefinition<Model.Order> exp;
        string CustomerTaxID = string.Empty;
        string CustomerName = string.Empty;
        string PromoCode = string.Empty;
        Double? TotalPrice = null;
        if (!string.IsNullOrWhiteSpace(filter))
        {
            var conditions = filter.Split(",");
            if (conditions.Count() >= 1)
            {
                foreach (var condition in conditions)
                {
                    var slice = condition?.Split("=");
                    if (slice.Length > 1)
                    {
                        var field = slice[0];
                        var value = slice[1];
                        if (field.ToLower() == "customertaxid")
                            CustomerTaxID = value;
                        else if (field.ToLower() == "customername")
                            CustomerName = value;
                        else if (field.ToLower() == "promocode")
                            PromoCode = value;
                        else if (field.ToLower() == "totalprice")
                            TotalPrice = Convert.ToDouble(value);
                    }
                }
            }
        }
        var bfilter = builder.Empty;
        if (!string.IsNullOrWhiteSpace(CustomerTaxID))
        {
            var CustomerTaxIDFilter = builder.Eq(x => x.CustomerTaxID, CustomerTaxID);
            bfilter &= CustomerTaxIDFilter;
        }
        if (!string.IsNullOrWhiteSpace(CustomerName))
        {
            var CustomerNameFilter = builder.Eq(x => x.CustomerName, CustomerName);
            bfilter &= CustomerNameFilter;
        }
        if (!string.IsNullOrWhiteSpace(PromoCode))
        {
            var PromoCodeFilter = builder.Eq(x => x.PromoCode, PromoCode);
            bfilter &= PromoCodeFilter;
        }
        if (TotalPrice != null)
        {
            var TotalPriceFilter = builder.Eq(x => x.TotalPrice, TotalPrice);
            bfilter &= TotalPriceFilter;
        }
        exp = bfilter;
        return exp;
    }
    public bool Exists(Guid orderID)
    {
        var result = Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var order = state.Order.Find(x => x.ID == orderID).Project<Model.Order>("{ ID: 1 }").FirstOrDefault();
            return (orderID == order?.ID);
        });
        if (result is null)
            return false;
        return result;
    }
    public long Total(string filter)
    {
        return Dp.Pipeline(ExecuteResult: (stateContext) =>
        {
            var state = new ConnectionMongo(stateContext, Dp);
            var total = state.Order.Find(GetFilter(filter)).CountDocuments();
            return total;
        });
    }

#endregion Read

#region mappers
    public static DevPrime.State.Repositories.Order.Model.Order ToState(Domain.Aggregates.Order.Order order)
    {
        if (order is null)
            return new DevPrime.State.Repositories.Order.Model.Order();
        DevPrime.State.Repositories.Order.Model.Order _order = new DevPrime.State.Repositories.Order.Model.Order();
        _order.ID = order.ID;
        _order.CustomerTaxID = order.CustomerTaxID;
        _order.CustomerName = order.CustomerName;
        _order.PromoCode = order.PromoCode;
        _order.Items = ToState(order.Items);
        _order.TotalPrice = order.TotalPrice;
        return _order;
    }
    public static DevPrime.State.Repositories.Order.Model.Item ToState(Domain.Aggregates.Order.Item item)
    {
        if (item is null)
            return new DevPrime.State.Repositories.Order.Model.Item();
        DevPrime.State.Repositories.Order.Model.Item _item = new DevPrime.State.Repositories.Order.Model.Item();
        _item.ID = item.ID;
        _item.Amount = ToState(item.Amount);
        _item.ParcialPrice = item.ParcialPrice;
        _item.Product = item.Product;
        return _item;
    }
    public static DevPrime.State.Repositories.Order.Model.Mesure ToState(Domain.Aggregates.Order.Mesure mesure)
    {
        if (mesure is null)
            return new DevPrime.State.Repositories.Order.Model.Mesure();
        DevPrime.State.Repositories.Order.Model.Mesure _mesure = new DevPrime.State.Repositories.Order.Model.Mesure();
        _mesure.UnitOfMesure = mesure.UnitOfMesure.ToString();
        _mesure.Quantity = mesure.Quantity;
        return _mesure;
    }
    public static List<DevPrime.State.Repositories.Order.Model.Mesure> ToState(IList<Domain.Aggregates.Order.Mesure> mesureList)
    {
        List<DevPrime.State.Repositories.Order.Model.Mesure> _mesureList = new List<DevPrime.State.Repositories.Order.Model.Mesure>();
        if (mesureList != null)
        {
            foreach (var mesure in mesureList)
            {
                DevPrime.State.Repositories.Order.Model.Mesure _mesure = new DevPrime.State.Repositories.Order.Model.Mesure();
                _mesure.UnitOfMesure = mesure.UnitOfMesure.ToString();
                _mesure.Quantity = mesure.Quantity;
                _mesureList.Add(_mesure);
            }
        }
        return _mesureList;
    }
    public static List<DevPrime.State.Repositories.Order.Model.Item> ToState(IList<Domain.Aggregates.Order.Item> itemList)
    {
        List<DevPrime.State.Repositories.Order.Model.Item> _itemList = new List<DevPrime.State.Repositories.Order.Model.Item>();
        if (itemList != null)
        {
            foreach (var item in itemList)
            {
                DevPrime.State.Repositories.Order.Model.Item _item = new DevPrime.State.Repositories.Order.Model.Item();
                _item.ID = item.ID;
                _item.Amount = ToState(item.Amount);
                _item.ParcialPrice = item.ParcialPrice;
                _item.Product = item.Product;
                _itemList.Add(_item);
            }
        }
        return _itemList;
    }
    public static Domain.Aggregates.Order.Order ToDomain(DevPrime.State.Repositories.Order.Model.Order order)
    {
        if (order is null)
            return new Domain.Aggregates.Order.Order()
            {IsNew = true};
        Domain.Aggregates.Order.Order _order = new Domain.Aggregates.Order.Order(order.ID, order.CustomerTaxID, order.CustomerName, order.PromoCode, ToDomain(order.Items), order.TotalPrice);
        return _order;
    }
    public static Domain.Aggregates.Order.Item ToDomain(DevPrime.State.Repositories.Order.Model.Item item)
    {
        if (item is null)
            return new Domain.Aggregates.Order.Item()
            {IsNew = true};
        Domain.Aggregates.Order.Item _item = new Domain.Aggregates.Order.Item(item.ID, ToDomain(item.Amount), item.ParcialPrice, item.Product);
        return _item;
    }
    public static Domain.Aggregates.Order.Mesure ToDomain(DevPrime.State.Repositories.Order.Model.Mesure mesure)
    {
        if (mesure is null)
            return new Domain.Aggregates.Order.Mesure();
        Domain.Aggregates.Order.Mesure _mesure = new Domain.Aggregates.Order.Mesure(mesure.UnitOfMesure, mesure.Quantity);
        return _mesure;
    }
    public static List<Domain.Aggregates.Order.Mesure> ToDomain(IList<DevPrime.State.Repositories.Order.Model.Mesure> mesureList)
    {
        List<Domain.Aggregates.Order.Mesure> _mesureList = new List<Domain.Aggregates.Order.Mesure>();
        if (mesureList != null)
        {
            foreach (var mesure in mesureList)
            {
                Domain.Aggregates.Order.Mesure _mesure = new Domain.Aggregates.Order.Mesure(mesure.UnitOfMesure, mesure.Quantity);
                _mesureList.Add(_mesure);
            }
        }
        return _mesureList;
    }
    public static List<Domain.Aggregates.Order.Item> ToDomain(IList<DevPrime.State.Repositories.Order.Model.Item> itemList)
    {
        List<Domain.Aggregates.Order.Item> _itemList = new List<Domain.Aggregates.Order.Item>();
        if (itemList != null)
        {
            foreach (var item in itemList)
            {
                Domain.Aggregates.Order.Item _item = new Domain.Aggregates.Order.Item(item.ID, ToDomain(item.Amount), item.ParcialPrice, item.Product);
                _itemList.Add(_item);
            }
        }
        return _itemList;
    }
    public static List<Domain.Aggregates.Order.Order> ToDomain(IList<DevPrime.State.Repositories.Order.Model.Order> orderList)
    {
        List<Domain.Aggregates.Order.Order> _orderList = new List<Domain.Aggregates.Order.Order>();
        if (orderList != null)
        {
            foreach (var order in orderList)
            {
                Domain.Aggregates.Order.Order _order = new Domain.Aggregates.Order.Order(order.ID, order.CustomerTaxID, order.CustomerName, order.PromoCode, ToDomain(order.Items), order.TotalPrice);
                _orderList.Add(_order);
            }
        }
        return _orderList;
    }

#endregion mappers
}