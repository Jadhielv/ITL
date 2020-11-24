using KCTest.Domain.Common;
using System.Collections.Generic;

namespace KCTest.Infrastructure.Helpers
{
    public class HttpResponseHelper
    {
        public static HttpResponse NewHttpResponse(string message = "", string error = "", bool success = false)
        {
            return new HttpResponse
            {
                Error = error,
                Success = success,
                Message = message
            };
        }

        public static HttpResponseWithElement<T> NewHttpResponseWithElement<T>(T element = default(T), string message = "", string error = "", bool success = false)
        {
            return new HttpResponseWithElement<T>
            {
                Error = error,
                Success = success,
                Element = element,
                Message = message
            };
        }

        public static HttpResponseWithList<T> NewHttpResponseList<T>(IEnumerable<T> elements = default(IEnumerable<T>), string message = "", string error = "", bool success = false)
        {
            return new HttpResponseWithList<T>
            {
                Error = error,
                List = elements,
                Success = success,
                Message = message
            };
        }

        public static Result<T> NewResult<T>(HttpStatusCode statusCode, T httpResponse)
        {
            return new Result<T>
            {
                StatusCode = (int)statusCode,
                HttpResponse = httpResponse
            };
        }
    }
}
