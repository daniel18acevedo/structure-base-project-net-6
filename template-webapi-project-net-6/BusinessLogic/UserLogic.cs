using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicValidatorInterface;
using DataAccessInterface;
using Domain;

namespace BusinessLogic;
public class UserLogic : BaseLogic
{
    private readonly IRepository<User> _userRepository;
    private readonly IBusinessValidator<User> _userValidator;

    public UserLogic(
        IBusinessValidator<User> userValidator, 
        IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        this._userRepository = unitOfWork.GetRepository<User>();
        this._userValidator = userValidator;
    }

    public IEnumerable<User> GetCollection()
    {
        var users = this._userRepository.GetCollection();

        return users;
    }

    public User Add(User user)
    {
        this._userValidator.CreationValidation(user);

        this._userRepository.InsertAndSave(user);

        return user;
    }
}