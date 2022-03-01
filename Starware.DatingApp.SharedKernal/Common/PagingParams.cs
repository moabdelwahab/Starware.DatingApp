using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.SharedKernal.Common
{
    public class PagingParams
    {
        private const int MaxPageSize = 50;
        
        private int pageSize = 10;
        public int PageNumber { get; set; } = 1;
        
        public int PageSize {

            get => pageSize; 
            set => pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
