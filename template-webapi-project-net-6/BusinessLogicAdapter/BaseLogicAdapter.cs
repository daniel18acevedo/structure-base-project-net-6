using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using BusinessLogicFilter;
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

    public async Task<PagedList<dynamic>> GetCollectionAsync(PaginationFilter<TEntity> paginationFilter)
    {
        var elements = await this.GetElementsFromLogicAsync(paginationFilter);

        return elements;
    }

    protected virtual async Task<PagedList<dynamic>> GetElementsFromLogicAsync(PaginationFilter<TEntity> paginationFilter)
    {
        var elements = await this._entityLogic.GetCollectionAsync(paginationFilter);

        return elements;
    }

    public async Task<TDetailModel> CreateAsync<TDetailModel>(TModel model)
    {
        await this._modelBusinessValidator.CreationValidationAsync(model);
        
        var entity = this._mapper.Map<TEntity>(model);

        var entityCreated = await this.CreateEntityAsync(entity);

        var entityConverted = this._mapper.Map<TDetailModel>(entityCreated);

        return entityConverted;
    }

    protected virtual async Task<TEntity> CreateEntityAsync(TEntity entity)
    {
        var entityCreated = await this._entityLogic.AddAsync(entity);

        return entityCreated;
    }
}