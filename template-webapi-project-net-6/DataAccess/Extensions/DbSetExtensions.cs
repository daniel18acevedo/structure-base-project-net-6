using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DataAccess.Extensions
{
    internal static class DbSetExtensions
    {
        public static IQueryable<T> TrackElements<T>(this DbSet<T> elements, bool track) where T : class
        {
            return track ? elements : elements.AsNoTracking();
        }

        public static IQueryable<T> NullableInclude<T>(this IQueryable<T> elements, Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
        {
            return include is null ? elements : include(elements);
        }

        public static IQueryable<T> NullableOrderBy<T>(this IQueryable<T> elements, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            return orderBy is null ? elements : orderBy(elements);
        }

        public static IQueryable<T> NullableSelect<T>(this IQueryable<T> elements, Expression<Func<T, T>> select)
        {
            return select is null ? elements : elements.Select(select);
        }

        public static IQueryable<T> NullableWhere<T>(this IQueryable<T> elements, Expression<Func<T, bool>> condition)
        {
            return condition is null ? elements : elements.Where(condition);
        }
    }
}