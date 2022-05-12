using DataAccessInterface.Collections;

namespace BusinessLogicMapperInterface;
public interface IMap
{
    TDestination Map<TDestination>(object source);
    PagedList<dynamic> Map(object source);
}
