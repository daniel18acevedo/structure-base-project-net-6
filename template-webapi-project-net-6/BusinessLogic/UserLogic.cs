using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicFilter;
using BusinessLogicValidatorInterface;
using DataAccessInterface;
using Domain;

namespace BusinessLogic;
public class UserLogic : BaseLogic<User>
{
    private readonly IBusinessValidator<UserFilter> _userFilterValidator;

    public UserLogic(
        IUnitOfWork unitOfWork,
        IBusinessValidator<User> userValidator,
        IBusinessValidator<UserFilter> userFilterValidator,
        IBusinessValidator<PaginationFilter<object>> paginationFilterValidator) : base(
            unitOfWork,
            userValidator,
            paginationFilterValidator)
    {
        this._userFilterValidator = userFilterValidator;
    }

    protected override async Task CheckFilterForEntityValidation(PaginationFilter<User> paginationFilter)
    {
        var userFilter = (UserFilter)paginationFilter;

        await this._userFilterValidator.CreationValidationAsync(userFilter);
    }
}