using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicFilter;
using Domain;
using FluentValidation;

namespace BusinessLogicValidator.Filter;
public class UserFilterValidator : BaseValidator<UserFilter>
{
    public UserFilterValidator()
    {
        base.RuleFor(user => user.Email)
        .EmailAddress()
        .WithMessage("The property 'email' has incorrect format.")
        .When(user => !string.IsNullOrEmpty(user.Email));
    }
}