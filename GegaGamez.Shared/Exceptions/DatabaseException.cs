using System.Runtime.Serialization;

namespace GegaGamez.Shared.Exceptions;

public class DatabaseException : Exception
{
    protected DatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public DatabaseException()
    {
    }

    public DatabaseException(string? message) : base(message)
    {
    }

    public DatabaseException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
