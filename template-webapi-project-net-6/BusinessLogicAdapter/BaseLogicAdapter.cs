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
    protected readonly IBusinessValidator<PaginationFilter<object>> _paginationFilterValidator;
    
    public BaseLogicAdapter(
        BaseLogic<TEntity> entityLogic,
        IMap mapper,
        IBusinessValidator<TModel> modelBusinessValidator,
        IBusinessValidator<PaginationFilter<object>> paginationFilterValidator)
    {
        this._entityLogic = entityLogic;
        this._mapper = mapper;
        this._modelBusinessValidator = modelBusinessValidator;
        this._paginationFilterValidator = paginationFilterValidator;
    }

    public async Task<PagedList<dynamic>> GetCollectionAsync<TBasicModel>(PaginationFilter<TEntity> paginationFilter)
    {
        await this.CheckFilterValidation(paginationFilter);

        await this.CheckFilterForEntityValidation(paginationFilter);

        this.SetProperProperties<TBasicModel>(paginationFilter);

        var elements = await this.GetElementsFromLogicAsync(paginationFilter);

        return elements;
    }

    private async Task CheckFilterValidation(PaginationFilter<TEntity> paginationFilter)
    {
        var paginationFilterObject = new PaginationFilter<object>
        {
            Count = paginationFilter.Count,
            Page = paginationFilter.Page,
            Order = paginationFilter.Order,
        };

        await this._paginationFilterValidator.CreationValidationAsync(paginationFilterObject);
    }

    protected virtual Task CheckFilterForEntityValidation(PaginationFilter<TEntity> paginationFilter) { return Task.CompletedTask; }

    private void SetProperProperties<TBasicModel>(PaginationFilter<TEntity> paginationFilter)
    {
        if (paginationFilter.Data == null || !paginationFilter.Data.Any())
        {
            var basicType = typeof(TBasicModel);
            paginationFilter.Data = basicType.GetProperties().Select(property => property.Name).ToArray();
        }
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