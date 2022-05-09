using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Session;
using SessionInterface;

namespace Factory;
internal static class SessionFactory
{
    public static void InjectSession(this IServiceCollection services)
    {
        services.AddScoped<ISessionService, SessionService>();
    }
}