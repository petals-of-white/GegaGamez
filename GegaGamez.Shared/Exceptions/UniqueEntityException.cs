using GegaGamez.Shared.Entities;

namespace GegaGamez.Shared.Exceptions;

/// <summary>
/// Thrown when trying to add a new entity that violates unique constraint
/// </summary>
public class UniqueEntityException : ArgumentException
{
    private static string GenerateMsgForKey(IEntity entity)
    {
        var msg = $"Can not add new {entity.GetType().Name} with key {entity.Id} " +
            $"entry due to unique constraint. See the inner exceptioon for details";

        return msg;
    }

    public UniqueEntityException()
    {
    }

    public UniqueEntityException(string? message) : base(message)
    {
    }

    public UniqueEntityException(IEntity entity, Exception? innerException) : base(GenerateMsgForKey(entity), innerException)
    {
    }

    public UniqueEntityException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public UniqueEntityException(string? message, string? paramName) : base(message, paramName)
    {
    }

    public UniqueEntityException(string? message, string? paramName, Exception? innerException) : base(message, paramName, innerException)
    {
    }
}
