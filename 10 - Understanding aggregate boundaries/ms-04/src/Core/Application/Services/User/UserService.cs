namespace Application.Services.User;
public class UserService : ApplicationService<IUserState>, IUserService
{
    public UserService(IUserState state, IDp dp) : base(state, dp)
    {
    }
    public void Add(Model.User command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var user = command.ToDomain();
            Dp.Attach(user);
            user.Add();
        });
    }
    public void Update(Model.User command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var user = command.ToDomain();
            Dp.Attach(user);
            user.Update();
        });
    }
    public void Delete(Model.User command)
    {
        Dp.Pipeline(Execute: () =>
        {
            var user = command.ToDomain();
            Dp.Attach(user);
            user.Delete();
        });
    }
    public PagingResult<IList<Model.User>> GetAll(Model.User query)
    {
        return Dp.Pipeline(ExecuteResult: () =>
        {
            var user = query.ToDomain();
            Dp.Attach(user);
            var userList = user.Get(query.Limit, query.Offset, query.Ordering, query.Sort, query.Filter);
            var result = query.ToUserList(userList.Result, userList.Total, query.Offset, query.Limit);
            return result;
        });
    }
    public Model.User Get(Model.User query)
    {
        return Dp.Pipeline(ExecuteResult: () =>
        {
            var user = query.ToDomain();
            Dp.Attach(user);
            user = user.GetByID();
            var result = query.ToUser(user);
            return result;
        });
    }
}