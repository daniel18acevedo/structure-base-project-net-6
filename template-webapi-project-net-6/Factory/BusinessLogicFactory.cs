
using BusinessLogic;
using Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Factory;
internal static class BusinessLogicFactory
{
    public static void InjectBusinessLogics(this IServiceCollection services)
    {
        services.AddTransient<BaseLogic<User>, UserLogic>();
    }
}