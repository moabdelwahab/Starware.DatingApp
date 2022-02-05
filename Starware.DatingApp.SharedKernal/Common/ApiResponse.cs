using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Starware.DatingApp.SharedKernal.Common
{
    public class ApiResponse<T>  
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }


        public ApiResponse()
        {
            StatusCode = HttpStatusCode.OK; 
            Message = "Success";
            Data = default(T);
        }

        public ApiResponse(string message , T data , HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }
    }
}
