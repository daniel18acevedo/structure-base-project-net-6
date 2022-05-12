using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccessInterface.Collections;
using DataAccessInterface.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DataAccess.Extensions
{
    internal static class QueryableExtensions
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

        public static IQueryable<TEntity> SelectByClient<TEntity>(this IQueryable<TEntity> source, string[] properties)
        {
            var elementType = typeof(TEntity);

            // input parameter "o"
            var parameter = Expression.Parameter(elementType, "o");

            // new statement "new Data()"
            var elementCreated = Expression.New(elementType);

            // create initializers
            var bindings = properties.Where(property =>
            {
                bool exist = false;
                try
                {
                    elementType.GetProperty(property);

                    exist = true;
                }
                catch (ArgumentNullException) { }

                return exist;
            }
            ).Select(property =>
            {
                // property "Field1"
                var originalProperty = elementType.GetProperty(property);

                // original value "o.Field1"
                var callingProperty = Expression.Property(parameter, originalProperty);

                // set value "Field1 = o.Field1"
                return Expression.Bind(originalProperty, callingProperty);
            }
            );

            IQueryable<TEntity> elementsToReturn = null;

            if (bindings.Any())
            {
                // initialization "new Data { Field1 = o.Field1, Field2 = o.Field2 }"
                var elementInit = Expression.MemberInit(elementCreated, bindings);

                // expression "o => new Data { Field1 = o.Field1, Field2 = o.Field2 }"
                var lambda = Expression.Lambda<Func<TEntity, TEntity>>(elementInit, parameter);

                // compile to Func<Data, Data>
                elementsToReturn = source.Select(lambda);
            }

            return elementsToReturn;
        }

        public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> sourceElements, int pageIndex, int pageSize) where T : class
        {
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            pageSize = pageSize <= 0 ? 20 : pageSize;

            int totalSize = await sourceElements.CountAsync();
            int count = (pageIndex - 1) * pageSize;
            IEnumerable<T> elementesPaged = await sourceElements.Skip(count).Take(pageSize).ToListAsync();

            PagedList<T> pagedList = new PagedList<T>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalSize,
                Elements = elementesPaged,
                TotalPages = (int)Math.Ceiling(totalSize / (double)pageSize)
            };

            return pagedList;
        }

        public static PagedList<T> ToPagedList<T>(this IQueryable<T> sourceElements, int pageIndex, int pageSize) where T : class
        {
            var elementesPaged = sourceElements;
            var totalSize = sourceElements.Count();
            var count = totalSize;
            var totalPages = 1;
            var page = 1;
            var sizeOfPage = totalSize;

            if (pageIndex != 0 && pageSize != 0)
            {
                page = pageIndex < 0 ? 1 : pageIndex;
                sizeOfPage = pageSize < 0 ? 20 : pageSize;

                count = (pageIndex - 1) * pageSize;
                elementesPaged = elementesPaged.Skip(count).Take(pageSize);
                totalPages = (int)Math.Ceiling(totalSize / (double)pageSize);
            }

            PagedList<T> pagedList = new PagedList<T>
            {
                PageIndex = page,
                PageSize = sizeOfPage,
                TotalCount = totalSize,
                Elements = elementesPaged.ToList(),
                TotalPages = totalPages
            };

            return pagedList;
        }

        public static IQueryable<TEntity> OrderByClient<TEntity>(this IQueryable<TEntity> source, OrderConfig orderConfig)
        {
            IQueryable<TEntity> elementsOrdered = source;

            if (!(orderConfig is null) && orderConfig.Properties.Length != 0)
            {
                try
                {

                    string command = orderConfig.OrderBy == ORDER.DESC ? "OrderByDescending" : "OrderBy";
                    var type = typeof(TEntity);
                    var property = type.GetProperty(orderConfig.Properties[0]);
                    var parameter = Expression.Parameter(type, "p");
                    var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                    var orderByExpression = Expression.Lambda(propertyAccess, parameter);
                    var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },
                                                  source.Expression, Expression.Quote(orderByExpression));

                    elementsOrdered = source.Provider.CreateQuery<TEntity>(resultExpression);

                    string thenCommand = "ThenBy";
                    foreach (var thenByProperty in orderConfig.Properties)
                    {
                        var thenProperty = type.GetProperty(thenByProperty);
                        var thenPropertyAccess = Expression.MakeMemberAccess(parameter, thenProperty);
                        var thenByExpression = Expression.Lambda(thenPropertyAccess, parameter);
                        resultExpression = Expression.Call(typeof(Queryable), thenCommand, new Type[] { type, thenProperty.PropertyType },
                                                  elementsOrdered.Expression, Expression.Quote(thenByExpression));
                    }
                }
                catch (ArgumentNullException) { }
            }

            return elementsOrdered;
        }
    }
}