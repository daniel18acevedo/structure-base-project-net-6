using Microsoft.Extensions.DependencyInjection;
using SecurityFactory;

namespace Factory;
public static class SecurityFactory
{
    public static void InjectSecurity(this IServiceCollection services)
    {
        services.AddSecurityService();
    }
}