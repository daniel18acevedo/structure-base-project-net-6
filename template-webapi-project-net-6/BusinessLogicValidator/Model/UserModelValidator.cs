using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Model.Write;

namespace BusinessLogicValidator.Model;
public class UserModelValidator : BaseValidator<UserModel>
{
    public UserModelValidator()
    {
        base.RuleFor(user => user.Name)
        .NotNull()
        .WithMessage("Property 'name' can't be null")
        .NotEmpty()
        .WithMessage("Property 'name' can't be empty.");

        base.RuleFor(user => user.Email)
        .NotNull()
        .WithMessage("Property 'email' can't be null")
        .NotEmpty()
        .WithMessage("Property 'email' can't be empty.");
    }
}