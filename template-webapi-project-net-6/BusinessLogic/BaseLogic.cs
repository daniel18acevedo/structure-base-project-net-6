using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicFilter;
using BusinessLogicValidatorInterface;
using DataAccessDomain;
using DataAccessInterface;

namespace BusinessLogic;
public class BaseLogic<TEntity>
where TEntity : class
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IRepository<TEntity> _entityRepository;
    protected readonly IBusinessValidator<TEntity> _entityBusinessValidator;
    protected readonly IBusinessValidator<PaginationFilter<object>> _paginationFilterValidator;

    public BaseLogic(
        IUnitOfWork unitOfWork,
        IBusinessValidator<TEntity> entityBusinessValidator,
        IBusinessValidator<PaginationFilter<object>> paginationFilterValidator)
    {
        this._unitOfWork = unitOfWork;
        this._entityRepository = unitOfWork.GetRepository<TEntity>();
        this._entityBusinessValidator = entityBusinessValidator;
        this._paginationFilterValidator = paginationFilterValidator;
    }

    public async Task<PagedList<dynamic>> GetCollectionAsync(PaginationFilter<TEntity> paginationFilter)
    {
        await this.CheckFilterValidation(paginationFilter);

        await this.CheckFilterForEntityValidation(paginationFilter);

        var entities = await this._entityRepository.GetPagedCollectionAsync(
            condition: paginationFilter.Filter(),
            orderBy: new OrderConfig
            {
                OrderBy = paginationFilter.GetOrderType(),
                Properties = paginationFilter.OrderBy
            },
        selector: paginationFilter.Data,
        pageIndex: paginationFilter.Page,
        pageSize: paginationFilter.Count);

        return entities;
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

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await this._entityBusinessValidator.CreationValidationAsync(entity);

        await this._entityRepository.InsertAndSaveAsync(entity);

        return entity;
    }
}