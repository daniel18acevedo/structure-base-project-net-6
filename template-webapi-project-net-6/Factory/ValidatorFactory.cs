using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicValidator.Entities;
using BusinessLogicValidator.Model;
using BusinessLogicValidatorInterface;
using Domain;
using Microsoft.Extensions.DependencyInjection;
using Model.Write;

namespace Factory;
internal static class ValidatorFactory
{
    public static void InjectValidators(this IServiceCollection services)
    {
        services.AddTransient<IBusinessValidator<UserModel>, UserModelValidator>();
        services.AddTransient<IBusinessValidator<User>, UserValidator>();
    }
}