using Microsoft.Extensions.DependencyInjection;
using SecurityLogic;

namespace SecurityFactory;
public static class SecurityServiceFactory
{
    public static void AddSecurityService(this IServiceCollection services)
    {
        services.AddTransient<SecurityService>();
    }
}
