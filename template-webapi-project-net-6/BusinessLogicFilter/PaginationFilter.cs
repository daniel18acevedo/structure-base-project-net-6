using System.Linq.Expressions;
using DataAccessInterface.Entities;

namespace BusinessLogicFilter;
public class PaginationFilter<TEntity> where TEntity : class
{
    public int Count { get; set; }
    public int Page { get; set; }
    //asc desc
    public string? Order { get; set; }
    //properties
    public string[]? OrderBy { get; set; }
    //info to return
    public string[]? Data { get; set; }

    public Order GetOrderType()
    {
        var order = (Order)Enum.Parse(typeof(Order), this.Order, true);

        return order;
    }

    public virtual Expression<Func<TEntity, bool>> Filter()
    {
        return e => true;
    }
}