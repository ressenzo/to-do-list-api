namespace ToDoList.Domain.Entities.Interfaces;

public interface IEntity
{
    string Id { get; }

    IEnumerable<string> Errors { get; }

    bool IsValid();
}
