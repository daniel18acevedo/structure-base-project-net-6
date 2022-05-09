using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Context;
using DataAccess;
using DataAccessInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Factory;
internal static class DataAccessFactory
{
    public static void InjectDataAccess(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DbContext, MyContext>(options =>{
            options.UseSqlServer(connectionString);
        });
        services.AddTransient<IUnitOfWork, UnitOfWork>();
    }
}