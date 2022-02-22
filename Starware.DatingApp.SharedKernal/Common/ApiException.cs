using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.SharedKernal.Common
{
    public class ApiException
    {
        public ApiException(int statuCode, string message =null , string details = null)
        {
            StatuCode = statuCode;
            Message = message;
            Details = details;
        }

        public int StatuCode { get; set; }

        public string Message { get; set; }

        public string Details { get; set; }

    }
}
