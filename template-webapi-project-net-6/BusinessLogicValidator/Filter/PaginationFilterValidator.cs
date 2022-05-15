using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicFilter;
using DataAccessInterface.Entities;
using FluentValidation;

namespace BusinessLogicValidator.Filter;
public class PaginationFilterValidator<TEntity> : BaseValidator<PaginationFilter<TEntity>> where TEntity : class
{
    public PaginationFilterValidator()
    {
        base.RuleFor(pagination => pagination.Count)
        .GreaterThanOrEqualTo(0)
        .WithMessage("The property 'count' in the filter must be positive.");

        base.RuleFor(pagination => pagination.Page)
        .GreaterThanOrEqualTo(0)
        .WithMessage("The property 'count' in the filter must be positive.");

        base.RuleFor(pagination => pagination.Order)
        .IsEnumName(typeof(ORDER), false)
        .WithMessage("The property 'order' must be 'asc' or d'esc'")
        .When(pagination => pagination.Order != null);
    }
}