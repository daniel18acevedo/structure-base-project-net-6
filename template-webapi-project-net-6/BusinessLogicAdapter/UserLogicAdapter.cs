using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using BusinessLogicMapperInterface;
using BusinessLogicValidatorInterface;
using Domain;
using Model.Read;
using Model.Write;

namespace BusinessLogicAdapter;
public class UserLogicAdapter : BaseLogicAdapter<UserModel, User>
{
    public UserLogicAdapter(
        BaseLogic<User> userLogic,
        IBusinessValidator<UserModel> userModelValidator,
        IMap mapper) : base(userLogic, mapper, userModelValidator) { }
}