using System.Collections.Generic;
using System.Net;

namespace QPANC.Services.Abstract
{
    public class BaseResponse
    {
        public BaseResponse() { }
        public BaseResponse(HttpStatusCode statusCode)
        {
            this.StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; set; }
        public Dictionary<string, string> Errors { get; set; }
    }

    public class BaseResponse<T> : BaseResponse
    {
        public BaseResponse() : base() { }
        public BaseResponse(HttpStatusCode statusCode) : base(statusCode) { }

        public T Data { get; set; }
    }
}
