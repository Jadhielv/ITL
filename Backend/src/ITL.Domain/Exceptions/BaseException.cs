using ITL.Domain.Common;
using System;

namespace ITL.Domain.Exceptions;

public class BaseException : Exception
{
    public HttpStatusCode StatusCode { get; }

    public BaseException(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
    }

    public BaseException(HttpStatusCode statusCode, string message)
    : base(message)
    {
        StatusCode = statusCode;
    }
}
