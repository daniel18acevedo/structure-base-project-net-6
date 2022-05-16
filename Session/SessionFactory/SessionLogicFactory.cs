using Microsoft.Extensions.DependencyInjection;
using Session;
using SessionInterface;

namespace SessionFactory;
public static class SessionLogicFactory
{
    public static void AddSessionLogic(this IServiceCollection services)
    {
        services.AddScoped<ISessionService, SessionService>();
    }
}
