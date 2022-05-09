using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic;
using BusinessLogicValidatorInterface;
using Domain;
using Model.Read;
using Model.Write;

namespace BusinessLogicAdapter;
public class UserLogicAdapter : BaseLogicAdapter
{
    private readonly UserLogic _userLogic;
    private readonly IBusinessValidator<UserModel> _userModelValidator;

    public UserLogicAdapter(
        UserLogic userLogic, 
        IBusinessValidator<UserModel> userModelValidator,
        IMapper mapper) : base (mapper)
    {
        this._userLogic = userLogic;
        this._userModelValidator = userModelValidator;
    }

    public IEnumerable<UserBasicModel> GetCollection()
    {
        var users = this._userLogic.GetCollection();

        var usersConverted = this._mapper.Map<IEnumerable<UserBasicModel>>(users);

        return usersConverted;
    }

    public UserDetailInfoModel Create(UserModel user)
    {
        this._userModelValidator.CreationValidation(user);

        var userEntity = this._mapper.Map<User>(user);

        var userCreated = this._userLogic.Add(userEntity);

        var userConverted = this._mapper.Map<UserDetailInfoModel>(userCreated);

        return userConverted;
    }
}