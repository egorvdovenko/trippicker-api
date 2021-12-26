using System;
using System.Collections.Generic;

namespace trippicker_api.Pagination
{
    public class PagedList<T>
    {
        public int Page { get; }
        public int PageSize { get; }

        public List<T> Items { get; } = new List<T>();
        public int TotalItems { get; }
        public int TotalPages => PageSize == 0 ? 0 : (int)Math.Ceiling((decimal)TotalItems / PageSize);

        public PagedList(PageFilter pageFilter, int totalItems, List<T> items)
        {
            Page = pageFilter.Page;
            PageSize = pageFilter.PageSize;
            TotalItems = totalItems;
            Items = items;
        }
    }
}
