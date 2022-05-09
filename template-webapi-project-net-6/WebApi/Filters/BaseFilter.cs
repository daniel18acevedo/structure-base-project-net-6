using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters;
internal class BaseFilter : Attribute
{
    protected TService GetService<TService>(FilterContext context) where TService : class
    {
        var serviceType = typeof(TService);
        var serviceLogicObject = context.HttpContext.RequestServices.GetService(serviceType);
        var serviceLogic = serviceLogicObject as TService;

        return serviceLogic;
    }
}