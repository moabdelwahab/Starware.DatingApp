using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Starware.DatingApp.SharedKernal.Common
{
    public class PagedList<T> : List<T> where T : class
    {
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
     

        public PagedList(IEnumerable<T> items, int count, int currentPage, int pageSize)
        {
            TotalCount = count;
            TotalPages = (int)Math.Ceiling(count /(double)pageSize);
            CurrentPage = currentPage;
            PageSize = pageSize;
            AddRange(items);
        }


        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }


    }
}
