using DataAccessInterface.Entities;

namespace BusinessLogic;
public class PaginationFilter
{
    public int Count { get; set; }
    public int Page { get; set; }
    //asc desc
    public string Order { get; set; }
    //properties
    public string[] OrderBy { get; set; }
    //info to return
    public string[] Data { get; set; }

    public ORDER GetOrderType()
    {
        var order = (ORDER)Enum.Parse(typeof(ORDER), this.Order);

        return order;
    }
}