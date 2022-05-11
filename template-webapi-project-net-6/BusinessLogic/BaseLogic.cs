using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicValidatorInterface;
using DataAccessInterface;

namespace BusinessLogic;
public class BaseLogic<TEntity>
where TEntity : class
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IRepository<TEntity> _entityRepository;
    protected readonly IBusinessValidator<TEntity> _entityBusinessValidator;

    public BaseLogic(IUnitOfWork unitOfWork, IBusinessValidator<TEntity> entityBusinessValidator)
    {
        this._unitOfWork = unitOfWork;
        this._entityRepository = unitOfWork.GetRepository<TEntity>();
        this._entityBusinessValidator = entityBusinessValidator;
    }

    public IEnumerable<TEntity> GetCollection()
    {
        var entities = this._entityRepository.GetCollection();

        return entities;
    }

    public TEntity Add(TEntity entity)
    {
        this._entityBusinessValidator.CreationValidation(entity);

        this._entityRepository.InsertAndSave(entity);

        return entity;
    }
}