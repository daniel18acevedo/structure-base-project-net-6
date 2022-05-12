using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using BusinessLogicMapperInterface;
using BusinessLogicValidatorInterface;
using DataAccessInterface.Collections;
using Model;

namespace BusinessLogicAdapter;
public abstract class BaseLogicAdapter<TModel, TEntity>
where TModel : class
where TEntity : class
{
    protected readonly IMap _mapper;
    protected readonly IBusinessValidator<TModel> _modelBusinessValidator;
    protected readonly BaseLogic<TEntity> _entityLogic;

    public BaseLogicAdapter(BaseLogic<TEntity> entityLogic, IMap mapper, IBusinessValidator<TModel> modelBusinessValidator)
    {
        this._entityLogic = entityLogic;
        this._mapper = mapper;
        this._modelBusinessValidator = modelBusinessValidator;
    }

    public PagedList<dynamic> GetCollection(PaginationFilter paginationFilter)
    {
        var elements = this.GetElementsFromLogic(paginationFilter);

        return elements;
    }

    protected virtual PagedList<dynamic> GetElementsFromLogic(PaginationFilter paginationFilter)
    {
        var elements = this._entityLogic.GetCollection(paginationFilter);

        return elements;
    }

    public TDetailModel Create<TDetailModel>(TModel model)
    {
        this._modelBusinessValidator.CreationValidation(model);
        
        var entity = this._mapper.Map<TEntity>(model);

        var entityCreated = this.CreateEntity(entity);

        var entityConverted = this._mapper.Map<TDetailModel>(entityCreated);

        return entityConverted;
    }

    protected virtual TEntity CreateEntity(TEntity entity)
    {
        var entityCreated = this._entityLogic.Add(entity);

        return entityCreated;
    }
}