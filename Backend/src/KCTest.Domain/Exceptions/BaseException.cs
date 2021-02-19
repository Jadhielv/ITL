using KCTest.Domain.Common;
using System;

namespace KCTest.Domain.Exceptions
{
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
}