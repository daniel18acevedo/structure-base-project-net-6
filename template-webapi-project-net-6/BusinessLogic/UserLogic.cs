using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicValidatorInterface;
using DataAccessInterface;
using Domain;

namespace BusinessLogic;
public class UserLogic : BaseLogic<User>
{
    public UserLogic(
        IUnitOfWork unitOfWork,
        IBusinessValidator<User> userValidator
        ) : base(unitOfWork, userValidator) { }
}