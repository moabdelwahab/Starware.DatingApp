using Starware.DatingApp.SharedKernal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.Core.Requests
{
    public class GetAllUsersRequest : PagingParams
    {
        public string? Gender { get; set; }
        public string? Name { get; set; }
        public int? FromAge { get; set; }
        public int? ToAge { get; set; }
        public string? OrderBy { get; set; }

    }
}
