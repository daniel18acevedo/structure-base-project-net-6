using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccessInterface.Collections;
using DataAccessInterface.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace DataAccessInterface
{
    public interface IRepository<T> where T : class
    {
        Task Attach(T entity);
        Task<bool> ExistAsync(Expression<Func<T, bool>> condition);

        Task<T> GetAsync(
            Expression<Func<T, bool>> condition,
            Expression<Func<T, T>> selector = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
            bool track = false);

        Task<IEnumerable<T>> GetCollectionAsync(
            Expression<Func<T, bool>> condition = null,
            Expression<Func<T, T>> selector = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        Task<PagedList<dynamic>> GetPagedCollectionAsync(
        Expression<Func<T, bool>> condition = null,
        string[] selector = null,
        OrderConfig orderBy = null,
        int pageIndex = 0,
        int pageSize = 0);

        Task InsertAndSaveAsync(T entity);
        Task SaveChangesAsync();
        Task UpdateAsync(T entity);
        Task UpdateAndSaveAsync(T entity);
        Task LoadAsync<TProperty>(T entity, Expression<Func<T, TProperty>> propertyExpression) where TProperty : class;
        Task DeleteAsync(T entity);
        Task DeleteByIdsAndSaveAsync(IEnumerable<object> ids);
        Task DeleteByIdAndSaveAsync(object id);
    }
}