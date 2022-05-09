
using BusinessLogic;
using Microsoft.Extensions.DependencyInjection;

namespace Factory;
internal static class BusinessLogicFactory
{
    public static void InjectBusinessLogics(this IServiceCollection services)
    {
        services.AddTransient<UserLogic>();
    }
}