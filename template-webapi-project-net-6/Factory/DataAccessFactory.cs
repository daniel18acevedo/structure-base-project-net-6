using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Context;
using DataAccessFactory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Factory;
internal static class DataAccessFactory
{
    public static void InjectDataAccess(this IServiceCollection services, string connectionString)
    {
        connectionString = Environment.GetEnvironmentVariable(connectionString);
        
        services.AddDbContext<DbContext, MyContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        
        services.AddDataAccessService();
    }
}