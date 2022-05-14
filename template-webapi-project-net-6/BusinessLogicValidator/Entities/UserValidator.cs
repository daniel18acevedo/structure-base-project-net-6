using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessInterface;
using Domain;
using FluentValidation;

namespace BusinessLogicValidator.Entities;
public class UserValidator : BaseValidator<User>
{
    private readonly IRepository<User> _userRepository;

    public UserValidator(IUnitOfWork unitOfWork)
    {
        this._userRepository = unitOfWork.GetRepository<User>();

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


    protected override async Task BusinessValidation(User user)
    {
        var existUserWithThatEmail = await this._userRepository.ExistAsync(userSaved => userSaved.Email == user.Email);

        if(existUserWithThatEmail)
        {
            throw new ArgumentException("That email is in use");
        }
    }
}