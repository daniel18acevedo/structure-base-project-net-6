namespace BusinessLogicMapperInterface;
public interface IMap
{
    TDestination Map<TDestination>(object source);
}
