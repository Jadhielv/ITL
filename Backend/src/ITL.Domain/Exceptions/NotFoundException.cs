using KCTest.Domain.Common;

namespace KCTest.Domain.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException() : base(HttpStatusCode.NotFound)
        {
        }

        public NotFoundException(string message) : base(HttpStatusCode.NotFound, message)
        {
        }
    }
}