using Domain;

namespace Context;
public class UserQuery
{
    public IQueryable<User> GetUsers => new List<User>().AsQueryable();
}