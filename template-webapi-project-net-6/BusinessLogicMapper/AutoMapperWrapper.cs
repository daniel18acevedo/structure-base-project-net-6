using AutoMapper;
using BusinessLogicMapperInterface;

namespace BusinessLogicMapper;
public class AutoMapperWrapper : IMap
{
    private readonly IMapper _autoMapper;

    public AutoMapperWrapper(IMapper mapper)
    {
        this._autoMapper = mapper;
    }

    public TDestination Map<TDestination>(object source)
    {
        var elementConverted = this._autoMapper.Map<TDestination>(source);

        return elementConverted;
    }
}
