using trippicker_api.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace trippicker_api.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> query, PageFilter pageFilter)
        {
            var total = await query.CountAsync();
            var skip = (pageFilter.Page - 1) * pageFilter.PageSize;
            var items = await query
                .Skip(skip)
                .Take(pageFilter.PageSize)
                .ToListAsync();

            return new PagedList<T>(pageFilter, total, items);
        }
    }
}
