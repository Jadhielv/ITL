using ITL.Domain.Common;

namespace ITL.Domain.Exceptions;

public class ConflictException : BaseException
{
    public ConflictException() : base(HttpStatusCode.Conflict)
    {
    }

    public ConflictException(string message) : base(HttpStatusCode.Conflict, message)
    {
    }
}
