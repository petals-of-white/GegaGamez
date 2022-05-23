using GegaGamez.Shared.Entities;

namespace GegaGamez.Shared.Exceptions;

/// <summary>
/// The exception that is thrown when trying to insert a new record with non-existent child-record
/// </summary>
public class EntityNotFoundException : KeyNotFoundException
{
    private static string GenerateMsg(IEntity entity)
    {
        var msg = $"{entity.GetType().Name} with id {entity.Id} does not exist.";
        return msg;
    }

    public EntityNotFoundException()
    {
    }

    public EntityNotFoundException(string? message) : base(message)
    {
    }

    public EntityNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public EntityNotFoundException(IEntity entity, Exception? innerException) : base(GenerateMsg(entity), innerException)
    {
    }

    public IEntity? Entity { get; init; }
}
