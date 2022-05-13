using DataAccessInterface.Collections;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Extensions;

public static class PagedExtension
{
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
}