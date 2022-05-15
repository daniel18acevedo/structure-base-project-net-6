using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicFilter;
using BusinessLogicValidatorInterface;
using DataAccessInterface;
using DataAccessInterface.Collections;
using DataAccessInterface.Entities;

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

    public async Task<PagedList<dynamic>> GetCollectionAsync(PaginationFilter<TEntity> paginationFilter)
    {
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

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await this._entityBusinessValidator.CreationValidationAsync(entity);

        await this._entityRepository.InsertAndSaveAsync(entity);

        return entity;
    }
}