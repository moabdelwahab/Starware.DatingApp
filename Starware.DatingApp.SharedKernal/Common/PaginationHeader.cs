using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.SharedKernal.Common
{
    public class PaginationHeader
    {
        public PaginationHeader(int totalCount, int totalPages, int pageSize, int pageNumber)
        {
            TotalCount = totalCount;
            TotalPages = totalPages;
            PageSize = pageSize;
            PageNumber = pageNumber;
        }

        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

    }
}
