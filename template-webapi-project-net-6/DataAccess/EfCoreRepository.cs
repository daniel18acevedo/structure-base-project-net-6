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

    public void Attach(T entity)
    {
        this._context.Attach(entity);
    }

    public bool Exist(Expression<Func<T, bool>> condition)
    {
        bool existEntity = this._elements.AsNoTracking().Any(condition);

        return existEntity;
    }

    public T Get(
        Expression<Func<T, bool>> condition,
        Expression<Func<T, T>> selector = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
        bool track = false)
    {
        var entity = this._elements.TrackElements(track).Where(condition).NullableInclude(include).NullableSelect(selector).First();

        return entity;
    }

    public void InsertAndSave(T entity)
    {
        this._elements.Add(entity);
        this.SaveChanges();
    }

    public void Insert(T entity)
    {
        this._elements.Add(entity);
    }

    public void Update(T entity)
    {
        this._context.Update(entity);
    }

    public void UpdateAndSave(T entity)
    {
        this._context.Entry<T>(entity).State = EntityState.Modified;

        this.SaveChanges();
    }

    public void Load<TProperty>(T entity, Expression<Func<T, TProperty>> propertyExpression) where TProperty : class
    {
        this._context.Entry(entity).Reference(propertyExpression).Load();
    }

    public void SaveChanges()
    {
        this._context.SaveChanges();
    }

    public IEnumerable<T> GetCollection(
        Expression<Func<T, bool>> condition = null,
        Expression<Func<T, T>> selector = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
    {
        var elements = this._elements.AsNoTracking().NullableWhere(condition).NullableInclude(include).NullableOrderBy(orderBy).NullableSelect(selector);

        return elements;
    }

    

    public PagedList<T> GetPagedCollection(
        Expression<Func<T, bool>> condition = null, 
        string[] selector = null, 
        OrderConfig orderBy = null, 
        int pageIndex = 0, 
        int pageSize = 0)
    {
        var paginatedElements = this._elements.AsNoTracking().NullableWhere(condition).OrderByClient(orderBy).SelectByClient(selector).ToPagedList(pageIndex, pageSize);

        return paginatedElements;
    }

    public void Delete(T entity)
    {
        this._context.Remove(entity);
    }

    public void DeleteByIdAndSave(object id)
    {
        this.DeleteByIdsAndSave(new List<object> { id });
    }

    public void DeleteByIdsAndSave(IEnumerable<object> ids)
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
        this._context.SaveChanges();
    }
}