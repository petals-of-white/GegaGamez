namespace GegaGamez.Shared.Exceptions;

/// <summary>
/// Thrown when some of the entity's properties does not satisfy validation and/or db contraints
/// </summary>
public class IncorrectValueException : ArgumentException
{
    public IncorrectValueException()
    {
    }

    public IncorrectValueException(string? message) : base(message)
    {
    }

    public IncorrectValueException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public IncorrectValueException(string? message, string? paramName) : base(message, paramName)
    {
    }

    public IncorrectValueException(string? message, string? paramName, Exception? innerException) : base(message, paramName, innerException)
    {
    }
}
