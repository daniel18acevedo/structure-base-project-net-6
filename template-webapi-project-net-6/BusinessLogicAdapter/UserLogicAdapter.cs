using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using BusinessLogicFilter;
using BusinessLogicMapperInterface;
using BusinessLogicValidatorInterface;
using Domain;
using Model.Read;
using Model.Write;

namespace BusinessLogicAdapter;
public class UserLogicAdapter : BaseLogicAdapter<UserModel, User>
{
    private readonly IBusinessValidator<UserFilter> _userFilterValidator;
    
    public UserLogicAdapter(
        BaseLogic<User> userLogic,
        IBusinessValidator<UserModel> userModelValidator,
        IBusinessValidator<UserFilter> userFilterValidator,
        IBusinessValidator<PaginationFilter<object>> paginationFilterValidator,
        IMap mapper) : base(
            userLogic,
            mapper,
            userModelValidator,
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