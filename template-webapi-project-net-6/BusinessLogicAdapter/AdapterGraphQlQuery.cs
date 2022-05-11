using HotChocolate;
using Model.Read;

namespace BusinessLogicAdapter;
public class AdapterGraphQlQuery
{
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IEnumerable<UserDetailInfoModel> GetUsersModel([Service] UserLogicAdapter service) =>
           service.GetCollection<UserDetailInfoModel>();
}