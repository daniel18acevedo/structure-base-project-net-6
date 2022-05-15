using DataAccessInterface.Collections;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Extensions;

public static class PagedExtension
{
     public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> sourceElements, int pageIndex, int pageSize) where T : class
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
                Elements = await elementesPaged.ToListAsync(),
                TotalPages = totalPages
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
}