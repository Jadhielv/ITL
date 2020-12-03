using KCTest.Domain.Common;
using System.Collections.Generic;

namespace KCTest.Infrastructure.Helpers
{
    public class ResponseHelper
    {
        public static Response NewResponse(string message = "", string error = "", bool success = false)
        {
            return new Response
            {
                Error = error,
                Success = success,
                Message = message
            };
        }

        public static ResponseWithElement<T> NewResponseWithElement<T>(T element = default(T), string message = "", string error = "", bool success = false)
        {
            return new ResponseWithElement<T>
            {
                Error = error,
                Success = success,
                Element = element,
                Message = message
            };
        }

        public static ResponseWithList<T> NewResponseList<T>(IEnumerable<T> elements = default(IEnumerable<T>), string message = "", string error = "", bool success = false)
        {
            return new ResponseWithList<T>
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
