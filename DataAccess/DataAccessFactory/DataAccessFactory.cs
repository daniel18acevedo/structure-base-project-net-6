using DataAccess;
using DataAccessInterface;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessFactory;
public static class DataAccessFactory
{
    public static void AddDataAccessService(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();
    }
}
