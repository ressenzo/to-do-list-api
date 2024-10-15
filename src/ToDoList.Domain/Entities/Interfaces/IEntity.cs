namespace ToDoList.Domain.Entities.Interfaces;

public interface IEntity
{
    Guid Id { get; }

    IEnumerable<string> Errors { get; }

    bool IsValid();
}
