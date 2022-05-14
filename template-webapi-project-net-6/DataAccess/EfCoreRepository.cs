using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using DataAccess.Extensions;
using DataAccessInterface;
using DataAccessInterface.Collections;
using DataAccessInterface.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DataAccess;
public class EfCoreRepository<T> : IRepository<T> where T : class
{
    private readonly DbContext _context;
    private readonly DbSet<T> _elements;

    public EfCoreRepository(DbContext context)
    {
        this._context = context;
        this._elements = context.Set<T>();
    }

    public async Task Attach(T entity)
    {
        this._context.Attach(entity);
    }

    public async Task<bool> ExistAsync(Expression<Func<T, bool>> condition)
    {
        bool existEntity = await this._elements.AsNoTracking().AnyAsync(condition);

        return existEntity;
    }

    public async Task<T> GetAsync(
        Expression<Func<T, bool>> condition,
        Expression<Func<T, T>> selector = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
        bool track = false)
    {
        var entity = await this._elements.TrackElements(track).Where(condition).NullableInclude(include).NullableSelect(selector).FirstAsync();

        return entity;
    }

    public async Task InsertAndSaveAsync(T entity)
    {
        await this._elements.AddAsync(entity);
        await this.SaveChangesAsync();
    }

    public async Task InsertAsync(T entity)
    {
        await this._elements.AddAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        this._context.Update(entity);
    }

    public async Task UpdateAndSaveAsync(T entity)
    {
        await this.UpdateAsync(entity);

        await this.SaveChangesAsync();
    }

    public async Task LoadAsync<TProperty>(T entity, Expression<Func<T, TProperty>> propertyExpression) where TProperty : class
    {
        await this._context.Entry(entity).Reference(propertyExpression).LoadAsync();
    }

    public async Task SaveChangesAsync()
    {
        await this._context.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> GetCollectionAsync(
        Expression<Func<T, bool>> condition = null,
        Expression<Func<T, T>> selector = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
    {
        var elements = await this._elements.AsNoTracking().NullableWhere(condition).NullableInclude(include).NullableOrderBy(orderBy).NullableSelect(selector).ToListAsync();

        return elements;
    }

    

    public async Task<PagedList<dynamic>> GetPagedCollectionAsync(
        Expression<Func<T, bool>> condition = null, 
        string[] selector = null, 
        OrderConfig orderBy = null, 
        int pageIndex = 0, 
        int pageSize = 0)
    {
        var paginatedElements = await this._elements.AsNoTracking().NullableWhere(condition).OrderByClient(orderBy).SelectByClientDynamic(selector).ToPagedListAsync(pageIndex, pageSize);

        return paginatedElements;
    }

    public async Task DeleteAsync(T entity)
    {
        this._context.Remove(entity);
    }

    public async Task DeleteByIdAndSaveAsync(object id)
    {
        this.DeleteByIdsAndSaveAsync(new List<object> { id });
    }

    public async Task DeleteByIdsAndSaveAsync(IEnumerable<object> ids)
    {
        var entities = new List<T>();

        var typeInfo = typeof(T).GetTypeInfo();
        var key = this._context.Model.FindEntityType(typeInfo).FindPrimaryKey().Properties.FirstOrDefault();
        var property = typeInfo.GetProperty(key?.Name);

        foreach (var id in ids)
        {
            T entity;
            if (property != null)
            {

                entity = Activator.CreateInstance<T>();
                property.SetValue(entity, id);

            }
            else
            {
                entity = this._elements.Find(id);
            }

            entities.Add(entity);
        }

        this._context.RemoveRange(entities);
        await this._context.SaveChangesAsync();
    }
}