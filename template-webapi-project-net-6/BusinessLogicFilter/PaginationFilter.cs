using System.Linq.Expressions;
using DataAccessDomain;

namespace BusinessLogicFilter;
public class PaginationFilter<TEntity> where TEntity : class
{
    public int Count { get; set; }
    public int Page { get; set; }
    //asc desc
    public string? Order { get; set; } = ORDER.ASC.ToString();
    //properties
    public string[]? OrderBy { get; set; } = new string[0];
    //info to return
    public string[]? Data { get; set; } = new string[0];

    public ORDER GetOrderType()
    {
        var order = (ORDER)Enum.Parse(typeof(ORDER), this.Order, true);

        return order;
    }

    public virtual Expression<Func<TEntity, bool>> Filter()
    {
        return e => true;
    }
}