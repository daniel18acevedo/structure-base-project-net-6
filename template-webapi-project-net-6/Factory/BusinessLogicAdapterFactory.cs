using BusinessLogicAdapter;
using BusinessLogicAdapter.AutoMapper;
using BusinessLogicMapper;
using BusinessLogicMapperInterface;
using Microsoft.Extensions.DependencyInjection;

namespace Factory;
internal static class BusinessLogicAdapterFactory
{
    public static void InjectBusinessLogicsAdapter(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UserProfile));
        services.AddTransient<IMap, AutoMapperWrapper>();
        services.AddTransient<UserLogicAdapter>();
    }
}
