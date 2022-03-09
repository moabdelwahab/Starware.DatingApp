using Starware.DatingApp.SharedKernal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.Core.Requests
{
    public class GetUserMessagesRequest :PagingParams
    {
        public string Container { get; set; }
        public string? Username { get; set; }
    }
}
