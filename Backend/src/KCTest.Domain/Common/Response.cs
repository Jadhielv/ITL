using System.Collections.Generic;

namespace KCTest.Domain.Common
{
    public class Result<T>
    {
        public T HttpResponse { get; set; }
        public int StatusCode { get; set; }
    }

    public class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
    }

    public class ResponseWithList<T> : Response
    {
        public IEnumerable<T> List { get; set; }
        public int Total { get; set; }
    }

    public class ResponseWithElement<T> : Response
    {
        public T Element { get; set; }
    }

    public enum HttpStatusCode
    {
        OK = 200,
        Created = 201,
        Accepted = 202,
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        MethodNotAllowed = 405,
        Conflict = 409,
        TooEarly = 425,
        InternalServerError = 500,
        BadGateway = 501,
        ServiceUnavailable = 503,
        HTTPVersionNotSupported = 505
    }
}
