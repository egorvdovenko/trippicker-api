using System.Collections.Generic;
using System.Linq;

namespace demo_web_api.Pagination
{
    public static class QueryableExtensions
    {
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> query, PageFilter pageFilter)
        {
            var total = query.Count();
            if (total == 0)
                return new PagedList<T>(pageFilter, total, new List<T>());

            var skip = (pageFilter.Page - 1) * pageFilter.PageSize;
            var items = query
                .Skip(skip)
                .Take(pageFilter.PageSize)
                .ToList();

            return new PagedList<T>(pageFilter, total, items);
        }
    }
}
