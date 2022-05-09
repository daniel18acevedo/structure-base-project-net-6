using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace DataAccessInterface
{
    public interface IRepository<T> where T : class
    {
        void Attach(T entity);
        bool Exist(Expression<Func<T, bool>> condition);

        T Get(
            Expression<Func<T, bool>> condition, 
            Expression<Func<T, T>> selector = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, 
            bool track = false);

        IEnumerable<T> GetCollection(
            Expression<Func<T, bool>> condition = null,
            Expression<Func<T, T>> selector = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);

        void InsertAndSave(T entity);
        void SaveChanges();
        void Update(T entity);
        void UpdateAndSave(T entity);
        void Load<TProperty>(T entity, Expression<Func<T, TProperty>> propertyExpression) where TProperty : class;
        void Delete(T entity);
        void DeleteByIdsAndSave(IEnumerable<object> ids);
        void DeleteByIdAndSave(object id);
    }
}