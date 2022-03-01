using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.Core.Requests
{
    public class GetLikesRequest : SharedKernal.Common.PagingParams
    {
        public string Predicate { get; set; }
        public int UserId { get; set; }
    }
}
